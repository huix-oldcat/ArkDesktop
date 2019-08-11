/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using Chromium.Event;

namespace ArkDesktopCSCefOsr
{
    partial class ArkDesktopBrowserControl
    {
        void LifeSpanHandler_OnAfterCreated(object sender, CfxOnAfterCreatedEventArgs e)
        {
            Manager.Browser = e.Browser;
            browser = e.Browser;
            browser.MainFrame.LoadUrl("about:version");
        }
    }
}
