/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Drawing;
using Chromium;

namespace ArkDesktopCSCefOsr
{
    public partial class ArkDesktopBrowserControl
    {
        internal CfxBrowser browser;

        private CfxClient client;
        private CfxLifeSpanHandler lifeSpanHandler;
        private CfxLoadHandler loadHandler;
        private CfxRenderHandler renderHandler;

        private Bitmap pixelBuffer;
        private object pbLock = new object();

        public ArkDesktopBrowserControl()
        { 
            #region Handler
            lifeSpanHandler = new CfxLifeSpanHandler();
            lifeSpanHandler.OnAfterCreated += LifeSpanHandler_OnAfterCreated;

            renderHandler = new CfxRenderHandler();
            //renderHandler.GetRootScreenRect += renderHandler_GetRootScreenRect;
            //renderHandler.GetScreenInfo += renderHandler_GetScreenInfo;
            //renderHandler.OnCursorChange += renderHandler_OnCursorChange;
            renderHandler.GetScreenPoint += RenderHandler_GetScreenPoint;
            renderHandler.GetViewRect += RenderHandler_GetViewRect;
            renderHandler.OnPaint += RenderHandler_OnPaint;

            loadHandler = new CfxLoadHandler();
            loadHandler.OnLoadError += LoadHandler_OnLoadError;
            loadHandler.OnLoadEnd += LoadHandler_OnLoadEnd;
            #endregion

            client = new CfxClient();
            client.GetLifeSpanHandler += (sender, e) => e.SetReturnValue(lifeSpanHandler);
            client.GetRenderHandler += (sender, e) => e.SetReturnValue(renderHandler);
            client.GetLoadHandler += (sender, e) => e.SetReturnValue(loadHandler);

            var settings = new CfxBrowserSettings();
            settings.BackgroundColor = new CfxColor(0, 0, 0, 0);

            var windowInfo = new CfxWindowInfo();
            windowInfo.SetAsWindowless(IntPtr.Zero);

            CfxBrowserHost.CreateBrowser(windowInfo, client, "about:blank", settings, null);
        }

        private double zoomPercent = 1;

        public double Zoom
        {
            get
            {
                return zoomPercent;
            }
            set
            {
                zoomPercent = value;
                browser.Host.ZoomLevel = Math.Log(value) / Math.Log(1.2d);
            }
        }

        public void OnResize()
        {
            if (browser != null)
            {
                browser.Host.WasResized();
            }
        }
    }
}
