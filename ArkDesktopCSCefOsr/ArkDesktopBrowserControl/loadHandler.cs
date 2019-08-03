/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using Chromium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Chromium.Event;

namespace ArkDesktopCSCefOsr
{
    partial class ArkDesktopBrowserControl
    {
        void LoadHandler_OnLoadError(object sender, CfxOnLoadErrorEventArgs e)
        {
            if (e.ErrorCode == CfxErrorCode.Aborted)
            {
                // this seems to happen when calling LoadUrl and the browser is not yet ready
                var url = e.FailedUrl;
                var frame = e.Frame;
                ThreadPool.QueueUserWorkItem((state) => {
                    Thread.Sleep(200);
                    frame.LoadUrl(url);
                });
            }
        }
    }
}
