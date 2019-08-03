/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Chromium;

namespace ArkDesktopCSCefOsr
{
    static public partial class Manager
    {
        private static Thread thread;
        private static object t_lock;
        private static CfxBrowser browser;
        public static CfxBrowser Browser
        {
            private get
            {
                return browser;
            }
            set
            {
                lock (t_lock)
                {
                    browser = value;
                }
            }
        }
        public static bool formShutdownFalg;
        public static Form mainForm, managerForm;

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        private static void RealInit()
        {
            managerForm = new ManagerForm();
            formShutdownFalg = false;
            Application.Run(managerForm);
        }
        public static void Init()
        {
            t_lock = new object();
            thread = new Thread(new ThreadStart(RealInit));
            thread.IsBackground = true;
            thread.Start();
        }

        public static void LoadUrl(string url)
        {
            lock (t_lock)
            {
                Browser.MainFrame.LoadUrl(url);
            }
        }
    }
}
