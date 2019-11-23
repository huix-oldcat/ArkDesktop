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
                settings.LogFile = "C:/Users/z1223/Desktop/log.txt";

                var app = new CfxApp();
                app.OnBeforeCommandLineProcessing += (s, e) =>
                {
                    // optimizations following recommendations from issue #84
                    e.CommandLine.AppendSwitch("disable-gpu");
                    e.CommandLine.AppendSwitch("disable-gpu-compositing");
                    e.CommandLine.AppendSwitch("disable-gpu-vsync");
                };
                app.OnRegisterCustomSchemes += App_OnRegisterCustomSchemes;
                if (!CfxRuntime.Initialize(settings, app))
                    return;
                CfxRuntime.RegisterSchemeHandlerFactory("akd", "test", new ResourceManager.SchemeHandlerFactory(Path.Combine(core.RootPath, "Resources\\")));
            }
            CreateThreads();
            manager = new ArkDesktopBrowserControl(window);
            while (!disposed) { CfxRuntime.DoMessageLoopWork(); }
        }

        private void App_OnRegisterCustomSchemes(object sender, Chromium.Event.CfxOnRegisterCustomSchemesEventArgs e)
        {
            bool t = e.Registrar.AddCustomScheme("akd", 1<<4 | 1 | 1 << 5);
        }
        // CEF_SCHEME_OPTION_STANDARD           = 1 << 0


        // CEF_SCHEME_OPTION_LOCAL              = 1 << 1
        // 将使用与应用于文件url相同的安全规则来处理该方案。普通页面无法链接或访问本地url。而且，默认情况下，本地URL只能对发出请求的同一个URL(源+路径)执行XMLHttpRequest调用。
        // 要允许从本地URL到具有相同源的其他URL的XMLHttpRequest调用，请设置CefSettings
        // 要允许从本地URL到所有源的XMLHttpRequest调用设置CefSettings universal_access_from_file_urls_allowed值为true(1)。

        // CEF_SCHEME_OPTION_DISPLAY_ISOLATED   = 1 << 2
        // 该模式只能从承载相同模式的其他内容中显示。
        // 例如，其他来源的页面无法使用该模式创建到url的iframe或超链接。对于必须从其他方案访问的方案不设置这个标志

        // CEF_SCHEME_OPTION_SECURE             = 1 << 3
        // 该方案将与应用于https url的安全规则相同。
        // 例如，从其他安全模式加载此模式将不会触发混合内容警告。

        // CEF_SCHEME_OPTION_CORS_ENABLED       = 1 << 4
        // 可以发送CORS请求。在设置CEF_SCHEME_OPTION_STANDARD的大多数情况下都应该设置这个值。

        // CEF_SCHEME_OPTION_CSP_BYPASSING      = 1 << 5
        // 方案可以绕过内容-安全-策略(CSP)检查。在设置CEF_SCHEME_OPTION_STANDARD的大多数情况下都不应该设置这个值。

        // CEF_SCHEME_OPTION_FETCH_ENABLED      = 1 << 6
        // 方案可以执行Fetch API请求


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
