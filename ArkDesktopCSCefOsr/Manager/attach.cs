using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkDesktopCSCefOsr
{
    static public partial class Manager
    {
        [DllImport("user32.dll", EntryPoint = "GetWindow")]
        private static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public static string GetHandleString(IntPtr ptr)
        {
            if (Environment.Is64BitProcess)
            {
                return ptr.ToInt64().ToString("X16");
            }
            else
            {
                return ptr.ToInt32().ToString("X8");
            }
        }

        public static IntPtr GetHandleFromString(string str)
        {
            if (Environment.Is64BitProcess)
            {
                return new IntPtr(Convert.ToInt64(str, 16));
            }
            else
            {
                return new IntPtr(Convert.ToInt32(str, 16));
            }
        }

        public static IntPtr FindProgman()
        {
            IntPtr ptr = FindWindow("Progman", "Program Manager");
            if(ptr != IntPtr.Zero)
            {
                IntPtr ptr1 = GetWindow(ptr, 5); // 5:The first child window
                if(ptr1 != IntPtr.Zero)
                {
                    return ptr1;
                }
            }
            return IntPtr.Zero;
        }

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        public static void SetMainFormParent(IntPtr ptr)
        {
            if (layeredWindow.InvokeRequired)
            {
                layeredWindow.Invoke((MethodInvoker)(() => SetMainFormParent(ptr)));
                return;
            }
            SetWindowLong(layeredWindow.Handle, -8, (uint)ptr.ToInt32());
        }
    }
}
