/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace ArkDesktop.CoreKit
{
    public class Win32
    {
        public enum Bool
        {
            False = 0,
            True
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct Point
        {
            public int x;
            public int y;

            public Point(int x, int y)
            { this.x = x; this.y = y; }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Size
        {
            public int cx;
            public int cy;

            public Size(int cx, int cy)
            { this.cx = cx; this.cy = cy; }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        struct ARGB
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        public const int ULW_COLORKEY = 0x00000001;
        public const int ULW_ALPHA = 0x00000002;
        public const int ULW_OPAQUE = 0x00000004;

        public const byte AC_SRC_OVER = 0x00;
        public const byte AC_SRC_ALPHA = 0x01;

        [DllImport("user32.dll ", ExactSpelling = true, SetLastError = true)]
        public static extern Bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref Point pptDst, ref Size psize, IntPtr hdcSrc, ref Point pprSrc, int crKey, ref BLENDFUNCTION pblend, int dwFlags);

        [DllImport("user32.dll ", ExactSpelling = true, SetLastError = true, EntryPoint = "GetDC")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll ", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll ", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll ", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll ", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll ", ExactSpelling = true, SetLastError = true)]
        public static extern Bool DeleteObject(IntPtr hObject);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(
            IntPtr hdcDest,//目标设备的句柄
            int nXDest,//目标对象的左上角x坐标
            int nYDest,//目标对象的左上角Y坐标
            int nWidth,//目标对象的矩形宽度
            int nHeight,//目标对象的矩形长度
            IntPtr hdcSrc,//源设备的句柄
            int nXSrc,//源对象的左上角x坐标
            int nYSrc,//源对象的左上角y坐标
            int dwRop//光栅的操作值
            );

        public enum TernaryRasterOperations : uint
        {
            SRCCOPY = 0x00CC0020,
            SRCPAINT = 0x00EE0086,
            SRCAND = 0x008800C6,
            SRCINVERT = 0x00660046,
            SRCERASE = 0x00440328,
            NOTSRCCOPY = 0x00330008,
            NOTSRCERASE = 0x001100A6,
            MERGECOPY = 0x00C000CA,
            MERGEPAINT = 0x00BB0226,
            PATCOPY = 0x00F00021,
            PATPAINT = 0x00FB0A09,
            PATINVERT = 0x005A0049,
            DSTINVERT = 0x00550009,
            BLACKNESS = 0x00000042,
            WHITENESS = 0x00FF0062,
            CAPTUREBLT = 0x40000000 //only if WinVer >= 5.0.0 (see wingdi.h)
        }//https://docs.microsoft.com/zh-cn/windows/win32/api/wingdi/nf-wingdi-bitblt
        [DllImport("user32.dll ", EntryPoint = " SendMessage ")]
        public static extern int SendMessage(int hWnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll ", EntryPoint = " ReleaseCapture ")]
        public static extern int ReleaseCapture();

        public const int WM_SysCommand = 0x0112;
        public const int SC_MOVE = 0xF012;

        public const int SC_MAXIMIZE = 61488;
        public const int SC_MINIMIZE = 61472;


        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);


        [DllImport("kernel32.dll")]
        public static extern int GetPrivateProfileInt(string lpAppName, string lpKeyName, int nDefault, string lpFileName);

        [DllImport("kernel32.dll")]
        public static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32.dll")]
        public static extern int WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);


        [DllImport("user32.dll", EntryPoint = "GetWindow")]
        public static extern IntPtr GetWindow(IntPtr hWnd, int uCmd);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_TRANSPARENT = 32;

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);
        [DllImport("user32.dll")]
        public static extern int EnumWindows(EnumWindowsProc ewp, int lParam);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        /*
         * HWND_TOP = 0; {在前面}
HWND_BOTTOM = 1; {在后面}
HWND_TOPMOST = HWND(-1); {在前面, 位于任何顶部窗口的前面}
HWND_NOTOPMOST = HWND(-2); {在前面, 位于其他顶部窗口的后面}
//uFlags 参数可选值:
SWP_NOSIZE = 1; {忽略 cx、cy, 保持大小}
SWP_NOMOVE = 2; {忽略 X、Y, 不改变位置}
SWP_NOZORDER = 4; {忽略 hWndInsertAfter, 保持 Z 顺序}
SWP_NOREDRAW = 8; {不重绘}
SWP_NOACTIVATE = $10; {不激活}
SWP_FRAMECHANGED = $20; {强制发送 WM_NCCALCSIZE 消息, 一般只是在改变大小时才发送此消息}
SWP_SHOWWINDOW = $40; {显示窗口}
SWP_HIDEWINDOW = $80; {隐藏窗口}
SWP_NOCOPYBITS = $100; {丢弃客户区}
SWP_NOOWNERZORDER = $200; {忽略 hWndInsertAfter, 不改变 Z 序列的所有者}
SWP_NOSENDCHANGING = $400; {不发出 WM_WINDOWPOSCHANGING 消息}
SWP_DRAWFRAME = SWP_FRAMECHANGED; {画边框}
SWP_NOREPOSITION = SWP_NOOWNERZORDER;{}
SWP_DEFERERASE = $2000; {防止产生 WM_SYNCPAINT 消息}
SWP_ASYNCWINDOWPOS = $4000; {若调用进程不拥有窗口, 系统会向拥有窗口的线程发出需求}
         */
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
    }
}
