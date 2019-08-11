using System;
using System.Windows.Forms;

namespace ArkDesktopCSCefOsr
{
    static public partial class Manager
    {
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
            IntPtr ptr = Win32.FindWindow("Progman", "Program Manager");
            if(ptr != IntPtr.Zero)
            {
                IntPtr ptr1 = Win32.GetWindow(ptr, 5); // 5:The first child window
                if(ptr1 != IntPtr.Zero)
                {
                    return ptr1;
                }
            }
            return IntPtr.Zero;
        }

        public static void SetMainFormParent(IntPtr ptr)
        {
            if (layeredWindow.InvokeRequired)
            {
                layeredWindow.Invoke((MethodInvoker)(() => SetMainFormParent(ptr)));
                return;
            }
            Win32.SetWindowLong(layeredWindow.Handle, -8, (uint)ptr.ToInt32());
        }
    }
}
