/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ArkDesktopCSCefOsr
{
    public partial class ArkDesktopBrowserControl
    {
        class ResourceHandler : Chromium.CfxResourceHandler
        {
            string mimeType;
            int done;
            byte[] buf;
            public ResourceHandler(string mineType, Stream stream)
            {
                GetResponseHeaders += Handler_GetResponseHeaders;
                ProcessRequest += Handler_ProcessRequest;
                ReadResponse += Handler_ReadResponse;
                mimeType = mineType;
                done = 0;
                buf = new byte[stream.Length];
                int bytesRead = 0, bytesToRead = (int)stream.Length;
                while(bytesToRead != 0)
                {
                    int read = stream.Read(buf, bytesRead, bytesToRead);
                    bytesRead += read;
                    bytesToRead -= read;
                }
            }

            private void Handler_ReadResponse(object sender, Chromium.Event.CfxReadResponseEventArgs e)
            {
                Marshal.Copy(buf, done, e.DataOut, e.BytesToRead); //TODO
                done += e.BytesToRead;
                e.BytesRead = e.BytesToRead;
                e.SetReturnValue(true);
            }

            private void Handler_ProcessRequest(object sender, Chromium.Event.CfxProcessRequestEventArgs e)
            {
                e.Callback.Continue();
                e.SetReturnValue(true);
            }

            private void Handler_GetResponseHeaders(object sender, Chromium.Event.CfxGetResponseHeadersEventArgs e)
            {
                e.Response.MimeType = mimeType;
                e.ResponseLength = buf.Length;
            }
        }
    }
}
