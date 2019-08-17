/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkDesktopCSCefOsr
{
    public partial class ArkDesktopBrowserControl
    {
        private void RequestHandler_GetResourceHandler(object sender, Chromium.Event.CfxGetResourceHandlerEventArgs e)
        {
            if (Manager.Resources.Redirects.ContainsKey(e.Request.Url))
            {
                Manager.Resources.Resource resource = Manager.Resources.Redirects[e.Request.Url];
                e.SetReturnValue(new ResourceHandler(resource.mimeType, new FileStream(resource.destPath.Replace("$(ResourceRoot)", Path.Combine(Environment.CurrentDirectory, "Resources")), FileMode.Open)));//TODO
            }
            else
            {
                e.SetReturnValue(null);
            }
        }
    }
}
