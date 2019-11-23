/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Drawing;
using Chromium;

namespace ArkDesktopCfx
{
    public partial class ArkDesktopBrowserControl
    {
        internal CfxBrowser browser;

        private CfxClient client;
        private CfxLifeSpanHandler lifeSpanHandler;
        private CfxLoadHandler loadHandler;
        private CfxRenderHandler renderHandler;
        private CfxRequestHandler requestHandler;

        private Bitmap pixelBuffer;
        private object pbLock = new object();

        public ArkDesktop.LayeredWindow window;

        public ArkDesktopBrowserControl(ArkDesktop.LayeredWindow window)
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

            requestHandler = new CfxRequestHandler();
            requestHandler.GetResourceHandler += RequestHandler_GetResourceHandler;
            #endregion

            client = new CfxClient();
            client.GetLifeSpanHandler += (sender, e) => e.SetReturnValue(lifeSpanHandler);
            client.GetRenderHandler += (sender, e) => e.SetReturnValue(renderHandler);
            //client.GetLoadHandler += (sender, e) => e.SetReturnValue(loadHandler);
            client.GetRequestHandler += (sender, e) => e.SetReturnValue(requestHandler);

            var settings = new CfxBrowserSettings();
            settings.BackgroundColor = new CfxColor(0, 0, 0, 0);

            var windowInfo = new CfxWindowInfo();
            windowInfo.SetAsWindowless(IntPtr.Zero);

            CfxBrowserHost.CreateBrowser(windowInfo, client, "http://www.bing.com/", settings, null);

            this.window = window;
            window.MouseDown += Window_MouseDown;
            window.MouseUp += Window_MouseUp;
            window.MouseMove += Window_MouseMove;
        }

        private void Window_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            browser.Host.SendMouseMoveEvent(new CfxMouseEvent { X = e.X, Y = e.Y }, false);
        }

        private void Window_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CfxMouseButtonType button;
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    button = CfxMouseButtonType.Left;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    button = CfxMouseButtonType.Right;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    button = CfxMouseButtonType.Middle;
                    break;
                default:
                    return;

            }
            browser.Host.SendMouseClickEvent(new CfxMouseEvent { X = e.X, Y = e.Y }, button, false, 1);
        }

        private void Window_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            CfxMouseButtonType button;
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    button = CfxMouseButtonType.Left;
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    button = CfxMouseButtonType.Right;
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    button = CfxMouseButtonType.Middle;
                    break;
                default:
                    return;

            }
            browser.Host.SendMouseClickEvent(new CfxMouseEvent { X = e.X, Y = e.Y }, button, true, 1);
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
