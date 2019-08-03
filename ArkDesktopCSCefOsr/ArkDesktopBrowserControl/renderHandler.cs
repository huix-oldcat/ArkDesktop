using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chromium;
using Chromium.Event;
using System.Drawing.Imaging;

namespace ArkDesktopCSCefOsr
{
    partial class ArkDesktopBrowserControl
    {
        #region Not implemented methods --- Useless
        //void renderHandler_UpdateDragCursor(object sender, CfxUpdateDragCursorEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //void renderHandler_StartDragging(object sender, CfxStartDraggingEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //void renderHandler_OnScrollOffsetChanged(object sender, CfxOnScrollOffsetChangedEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //void renderHandler_OnPopupSize(object sender, CfxOnPopupSizeEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //void renderHandler_OnPopupShow(object sender, CfxOnPopupShowEventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //void renderHandler_OnCursorChange(object sender, CfxOnCursorChangeEventArgs e)
        //{
        //    switch (e.Type)
        //    {
        //        case CfxCursorType.Hand:
        //            Cursor = Cursors.Hand;
        //            break;
        //        default:
        //            Cursor = Cursors.Default;
        //            break;
        //    }
        //}

        //void renderHandler_GetScreenPoint(object sender, CfxGetScreenPointEventArgs e)
        //{

        //    if (InvokeRequired)
        //    {
        //        Invoke((MethodInvoker)(() => renderHandler_GetScreenPoint(sender, e)));
        //        return;
        //    }

        //    if (!IsDisposed)
        //    {
        //        var origin = PointToScreen(new Point(e.ViewX, e.ViewY));
        //        e.ScreenX = origin.X;
        //        e.ScreenY = origin.Y;
        //        e.SetReturnValue(true);
        //    }
        //}

        //void renderHandler_GetScreenInfo(object sender, CfxGetScreenInfoEventArgs e)
        //{
        //}

        //void renderHandler_GetRootScreenRect(object sender, CfxGetRootScreenRectEventArgs e)
        //{
        //}
        #endregion

        void RenderHandler_OnPaint(object sender, CfxOnPaintEventArgs e)
        {

            lock (pbLock)
            {
                if (pixelBuffer == null || pixelBuffer.Width < e.Width || pixelBuffer.Height < e.Height)
                {
                    if (pixelBuffer != null)
                    {
                        pixelBuffer.Dispose();
                    }
                    pixelBuffer = new Bitmap(e.Width, e.Height, PixelFormat.Format32bppArgb);
                }
                using (Bitmap bm = new Bitmap(e.Width, e.Height, e.Width * 4, PixelFormat.Format32bppArgb, e.Buffer))
                using (Graphics g = Graphics.FromImage(pixelBuffer))
                {
                    g.DrawImageUnscaled(bm, 0, 0);
                }
            }
            Invalidate();
            //foreach (CfxRect r in e.DirtyRects)
            //{
            //    Invalidate(new Rectangle(r.X, r.Y, r.Width, r.Height));
            //}
        }

        void RenderHandler_GetViewRect(object sender, CfxGetViewRectEventArgs e)
        {

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => RenderHandler_GetViewRect(sender, e)));
                return;
            }

            if (!IsDisposed)
            {
                var origin = PointToScreen(new Point(0, 0));
                e.Rect.X = origin.X;
                e.Rect.Y = origin.Y;
                e.Rect.Width = Width;
                e.Rect.Height = Height;
            }
        }

        void RenderHandler_GetScreenPoint(object sender, CfxGetScreenPointEventArgs e)
        {

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => RenderHandler_GetScreenPoint(sender, e)));
                return;
            }

            if (!IsDisposed)
            {
                var origin = PointToScreen(new Point(e.ViewX, e.ViewY));
                e.ScreenX = origin.X;
                e.ScreenY = origin.Y;
                e.SetReturnValue(true);
            }
        }
    }
}
