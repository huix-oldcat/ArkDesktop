using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArkDesktop
{
    public class WindowPositionHelper
    {
        public static IntPtr GetDekstopLayerHwnd()
        {
            IntPtr retval = IntPtr.Zero;
            Win32.EnumWindows(new Win32.EnumWindowsProc(EnumWindowsProc), 0);
            return retval;

            bool EnumWindowsProc(IntPtr hWnd, int lParam)
            {
                StringBuilder sb = new StringBuilder(256);
                Win32.GetClassName(hWnd, sb, 256);
                if (sb.ToString() == "Progman" || sb.ToString() == "WorkerW")
                {
                    IntPtr child = Win32.FindWindowEx(hWnd, IntPtr.Zero, "SHELLDLL_DefView", null);
                    if (child != IntPtr.Zero)
                    {
                        retval = child;
                        return false;
                    }
                }
                return true;//这里必须返回1,返回0就不在枚举了
            }
        }

        public static IntPtr GetTaskbarHwnd()
        {
            return Win32.FindWindow("Shell_TrayWnd", null);
        }
    }
}
