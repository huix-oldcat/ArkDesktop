/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chromium.Event;

namespace ArkDesktopCSCefOsr
{
    partial class ArkDesktopBrowserControl
    {
        void LifeSpanHandler_OnAfterCreated(object sender, CfxOnAfterCreatedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => LifeSpanHandler_OnAfterCreated(sender, e)));
                return;
            }
            browser = e.Browser;
            browser.MainFrame.LoadUrl("http://akd.huix.cc/test1.html");
            if (Focused)
            {
                browser.Host.SendFocusEvent(true);
            }
        }
    }
}
