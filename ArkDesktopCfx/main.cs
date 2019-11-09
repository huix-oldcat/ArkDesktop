using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using ArkDesktop;
using Chromium;
using System.Windows.Forms;

namespace ArkDesktopCfx
{
    public class ArkDesktopCfx : IArkDesktopV2
    {
        public string Name => "ArkDesktopCfx";

        public string Description => "";//TODO

        public int Version => 1;

        public ArkDesktopBrowserControl manager;

        private Core core;
        private string additionalPath;
        private bool disposed = false;

        public LayeredWindow window;
        public LayeredWindowManager wManager;
        public CfxControl cManager;

        public void MainThread(object coreInst)
        {
            core = (Core)coreInst;
            additionalPath = Path.Combine(core.RootPath, "./Resources/ArkDesktop.Cfx/");
            if (CheckLibs() == false) return;
            int exitCode = CfxRuntime.ExecuteProcess(null);
            using (var settings = new CfxSettings())
            {
                settings.MultiThreadedMessageLoop = true;
                settings.WindowlessRenderingEnabled = true;
                settings.NoSandbox = true;
                settings.ResourcesDirPath = Path.Combine(additionalPath, "cef", "Resources");
                settings.LocalesDirPath = Path.Combine(additionalPath, "cef", "Resources", "locales");
                settings.BrowserSubprocessPath = Path.Combine(additionalPath, "ArkDesktopCfxSubProcess.exe");

                var app = new CfxApp();
                app.OnBeforeCommandLineProcessing += (s, e) =>
                {
                    // optimizations following recommendations from issue #84
                    e.CommandLine.AppendSwitch("disable-gpu");
                    e.CommandLine.AppendSwitch("disable-gpu-compositing");
                    e.CommandLine.AppendSwitch("disable-gpu-vsync");
                };
                if (!CfxRuntime.Initialize(settings, app))
                    return;
            }
            CreateThreads();
            manager = new ArkDesktopBrowserControl();
            manager.window = window;
            while (!disposed) { CfxRuntime.DoMessageLoopWork(); }
        }

        Thread WindowThread;

        private void CreateThreads()
        {
            window = new LayeredWindow();
            //WindowThread = new Thread(new ThreadStart(() => Application.Run(window)));
            //WindowThread.IsBackground = true;
            //WindowThread.SetApartmentState(ApartmentState.STA);
            //WindowThread.Start();

            wManager = new LayeredWindowManager();
            wManager.config = core.config;
            wManager.window = window;
            wManager.HelpPositionChange();
            core.AddControl("窗口控制", wManager);

            cManager = new CfxControl(this);
            core.AddControl("URL", cManager);
        }

        private bool CheckLibs()
        {
            if (!File.Exists(Path.Combine(additionalPath, "libcfx.dll")))
            {
                if (MessageBox.Show("并没有在软件目录下找到基本运行库\n您应该在获得这款软件的时候同时获得关于本软件依赖库的信息\n如果没有,您可以到本软件的Github Wiki获得帮助\n是否现在前往?",
                                "未能找到依赖库", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("https://github.com/huix-oldcat/ArkDesktop/wiki");
                }
                return false;
            }
            CfxRuntime.LibCefDirPath = Path.Combine(additionalPath, "cef");
            CfxRuntime.LibCfxDirPath = additionalPath;
            return true;
        }

        public void RequestDispose()
        {
        }
    }
}
