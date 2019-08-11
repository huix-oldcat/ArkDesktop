/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chromium;

namespace ArkDesktopCSCefOsr
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            string nowDir = Environment.CurrentDirectory;
            CfxRuntime.LibCefDirPath = System.IO.Path.Combine(nowDir, "cef");
            CfxRuntime.LibCfxDirPath = nowDir;
            int exitCode = CfxRuntime.ExecuteProcess(null);
            if (exitCode >= 0)
            {
                Environment.Exit(exitCode);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var settings = new CfxSettings())
            {
                settings.MultiThreadedMessageLoop = true;
                settings.WindowlessRenderingEnabled = true;
                settings.NoSandbox = true;
                settings.ResourcesDirPath = System.IO.Path.Combine(nowDir, "cef", "Resources");
                settings.LocalesDirPath = System.IO.Path.Combine(nowDir, "cef", "Resources", "locales");

                var app = new CfxApp();
                app.OnBeforeCommandLineProcessing += (s, e) =>
                {
                    // optimizations following recommendations from issue #84
                    e.CommandLine.AppendSwitch("disable-gpu");
                    e.CommandLine.AppendSwitch("disable-gpu-compositing");
                    e.CommandLine.AppendSwitch("disable-gpu-vsync");
                };
                if (!CfxRuntime.Initialize(settings, app))
                    Environment.Exit(-1);
            }

            Manager.layeredWindow = new LayeredWindow();
            Manager.control = new ArkDesktopBrowserControl();
            Manager.Init();

            Application.Run(Manager.layeredWindow);

            // CfxRuntime.QuitMessageLoop();
            CfxRuntime.Shutdown();
        }


        //static void Application_Idle(object sender, EventArgs e)
        //{
        //    CfxRuntime.DoMessageLoopWork();
        //}
    }
}
