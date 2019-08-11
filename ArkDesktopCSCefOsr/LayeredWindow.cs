/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ArkDesktopCSCefOsr
{
    public partial class LayeredWindow : Form
    {
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00080000;  //  WS_EX_LAYERED 扩展样式
                //无边框任务栏窗口最小化
                //const int WS_MINIMIZEBOX = 0x00020000;  // Winuser.h中定义
                ////CreateParams cp = base.CreateParams;
                //cp.Style = cp.Style | WS_MINIMIZEBOX;   // 允许最小化操作
                return cp;
            }
        }

        public bool drawBorder = false;

        public void SetBits(Bitmap bitmap)//调用UpdateLayeredWindow（）方法。this.BackgroundImage为你事先准备的带透明图片。
        {
            if(IsDisposed)
            {
                return;
            }

            if(InvokeRequired)
            {
                Invoke((MethodInvoker)(() => SetBits(bitmap)));
                return;
            }

            if (drawBorder)
            {
                Graphics g = Graphics.FromImage(bitmap);
                using(Pen pen = new Pen(Color.Red, 2))
                {
                    g.DrawRectangle(pen, new Rectangle(0, 0, bitmap.Width - 2 > 0 ? bitmap.Width - 2 : 1,
                                                       bitmap.Height - 2 > 0 ? bitmap.Height - 2 : 1));
                }
            }

            if (!Image.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Image.IsAlphaPixelFormat(bitmap.PixelFormat))
            {
                throw new ArgumentException("Not a 32-bit argb bitmap", nameof(bitmap));
            }

            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = Win32.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = Win32.CreateCompatibleDC(screenDC);

            try
            {
                Win32.Point topLoc = new Win32.Point(Left, Top);
                Win32.Size bitMapSize = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
                Win32.Point srcLoc = new Win32.Point(0, 0);

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = Win32.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = 255;
                blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                Win32.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, Win32.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDc, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
                Win32.ReleaseDC(IntPtr.Zero, screenDC);
                Win32.DeleteDC(memDc);
            }
        }
        public LayeredWindow()
        {
            InitializeComponent();
        }
    }
}
