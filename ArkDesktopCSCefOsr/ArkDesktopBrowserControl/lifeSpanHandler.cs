using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chromium.Event;

namespace ArkDesktopCSCefOsr
{
    partial class ArkDesktopBrowserControl
    {
        void LifeSpanHandler_OnAfterCreated(object sender, CfxOnAfterCreatedEventArgs e)
        {
            browser = e.Browser;
            browser.MainFrame.LoadUrl("http://akd.huix.cc/test1.html");
            if (Focused)
            {
                browser.Host.SendFocusEvent(true);
            }
        }
    }
}
