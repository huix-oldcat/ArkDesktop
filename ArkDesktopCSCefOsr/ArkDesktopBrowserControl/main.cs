/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chromium;

namespace ArkDesktopCSCefOsr
{
    partial class ArkDesktopBrowserControl : UserControl
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
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

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
            #endregion

            client = new CfxClient();
            client.GetLifeSpanHandler += (sender, e) => e.SetReturnValue(lifeSpanHandler);
            client.GetRenderHandler += (sender, e) => e.SetReturnValue(renderHandler);
            client.GetLoadHandler += (sender, e) => e.SetReturnValue(loadHandler);

            var settings = new CfxBrowserSettings();
            settings.BackgroundColor = new CfxColor(0, 0, 0, 0);

            var windowInfo = new CfxWindowInfo();
            windowInfo.SetAsWindowless(IntPtr.Zero);

            // Create handle now for InvokeRequired to work properly 
            CreateHandle();
            CfxBrowserHost.CreateBrowser(windowInfo, client, "about:blank", settings, null);
        }

        protected override void OnResize(EventArgs e)
        {
            if (browser != null)
            {
                browser.Host.WasResized();
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            base.OnPaintBackground(pevent);
            Graphics g = pevent.Graphics;
            g.Clear(Parent.BackColor);
            g.FillRectangle(new SolidBrush(Color.FromArgb(255, Color.Transparent)), ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            lock (pbLock)
            {
                if (pixelBuffer != null)
                    e.Graphics.DrawImage(pixelBuffer, e.ClipRectangle, e.ClipRectangle, GraphicsUnit.Pixel);
            }
        }
    }
}
