/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System.Windows.Forms;
using System.Threading;
using Chromium;
using System.IO;
using System;

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
        public static LayeredWindow layeredWindow;
        public static ManagerForm managerForm;
        public static ArkDesktopBrowserControl control;
        public static Thread cfxThread;
        public static bool startupFinished = false;

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
            cfxThread = new Thread(new ThreadStart(CfxRuntime.RunMessageLoop));
            cfxThread.IsBackground = true;
            cfxThread.Start();

            if (File.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml")))
            {
                Resources.LoadConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config.xml"));
            }
        }

        public static void LoadUrl(string url)
        {
            Thread.Sleep(500);
            lock (t_lock)
            {
                Browser.MainFrame.LoadUrl(url);
            }
        }
    }
}
