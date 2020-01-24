using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ArkDesktop.CoreKit
{
    public class LayeredWindow : Form
    {
        public Timer timer = new Timer();
        private IntPtr screenDC, memDC;
        public ManualResetEvent Ready { get; private set; } = new ManualResetEvent(false);

        public LayeredWindow()
        {
            Load += LayeredWindow_Load;
            Disposed += LayeredWindow_Disposed;
        }

        private void LayeredWindow_Disposed(object sender, EventArgs e)
        {
            Win32.ReleaseDC(IntPtr.Zero, screenDC);
            Win32.DeleteDC(memDC);
        }

        private void LayeredWindow_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            screenDC = Win32.GetDC(IntPtr.Zero);
            memDC = Win32.CreateCompatibleDC(screenDC);
            FormBorderStyle = FormBorderStyle.None;
            Ready.Set();
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x00080000;
                return cp;
            }
        }

        public void SetBits(Bitmap bitmap, byte transparency = 255)//调用UpdateLayeredWindow（）方法。this.BackgroundImage为你事先准备的带透明图片。
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => SetBits(bitmap)));
                return;
            }

            if (IsDisposed)
            {
                return;
            }

            if (!Image.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Image.IsAlphaPixelFormat(bitmap.PixelFormat))
            {
                throw new ArgumentException("Not a 32-bit argb bitmap", nameof(bitmap));
            }

            IntPtr oldBits = IntPtr.Zero;
            IntPtr hBitmap = IntPtr.Zero;

            try
            {
                Win32.Point topLoc = new Win32.Point(Left, Top);
                Win32.Size bitMapSize = new Win32.Size(bitmap.Width, bitmap.Height);
                Win32.BLENDFUNCTION blendFunc = new Win32.BLENDFUNCTION();
                Win32.Point srcLoc = new Win32.Point(0, 0);

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = Win32.SelectObject(memDC, hBitmap);

                blendFunc.BlendOp = Win32.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = transparency;
                blendFunc.AlphaFormat = Win32.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                Win32.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDC, ref srcLoc, 0, ref blendFunc, Win32.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    Win32.SelectObject(memDC, oldBits);
                    Win32.DeleteObject(hBitmap);
                }
            }
        }
    }
}
