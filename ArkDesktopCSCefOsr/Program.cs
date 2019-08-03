using System;
using System.Drawing;
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

            Form1 form = new Form1
            {
                //FormBorderStyle = System.Windows.Forms.FormBorderStyle.None,
                BackColor = Color.White,
                Height = 300,
                Width = 300,
                TransparencyKey = Color.White,
                AllowTransparency = true
            };

            ArkDesktopBrowserControl control = new ArkDesktopBrowserControl
            {
                Dock = DockStyle.Fill,
                Parent = form
            };
            form.Controls.Add(control);

            Application.Idle += Application_Idle;
            Application.Run(form);

            CfxRuntime.Shutdown();
        }


        static void Application_Idle(object sender, EventArgs e)
        {
            CfxRuntime.DoMessageLoopWork();
        }
    }
}
