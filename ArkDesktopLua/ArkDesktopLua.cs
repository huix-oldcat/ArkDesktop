using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ArkDesktop;
using LuaInterface;

namespace ArkDesktopLua
{
    public class ArkDesktopLua : IArkDesktopV2
    {
        public string Name => "ArkDesktop.Lua";
        public string Description => "An offical plugin that provides the ability to run lua script.";
        public int Version => 1;

        private bool isDisposed = false;
        public Core core;
        private LayeredWindow window;
        public LayeredWindowManager manager;
        private XElement config;
        private XNamespace ns = "ArkDesktop";

        private enum LaunchType
        {
            Positive, Passive
        }

        LaunchType launchType;

        public void MainThread(object coreInst)
        {
            core = (Core)coreInst;

            window = core.RequestPlugin("ArkDesktop.LayeredWindow").CreateInstance("ArkDesktop.LayeredWindow") as LayeredWindow;
            manager = core.RequestPlugin("ArkDesktop.LayeredWindowManager").CreateInstance("ArkDesktop.LayeredWindowManager") as LayeredWindowManager;
            manager.window = window;
            manager.config = core.config;
            manager.HelpPositionChange();
            manager.helpZoomChange = true;
            core.AddControl("渲染窗口", manager);

            if (EnsureConfigCorrect() == false)
            {
                return;
            }

            Lua lua = new Lua();
            LuaApi api = new LuaApi(this, lua);
            window.Click += (sender, e) => api.OnClick();

            while (true)
            {
                if (launchType == LaunchType.Positive)
                {
                    try
                    {
                        lua.DoString(config.Element(ns + "LuaScript").Value);
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("发生异常:" + e.Message + "\n" + e.StackTrace);
                    }
                }
                if (isDisposed)
                {
                    break;
                }
            }
        }

        private bool EnsureConfigCorrect()
        {
            if ((config = core.config.GetElement(ns + "Lua")) == null || core.config.GetElement(ns + "Lua") == null)
            {
                MessageBox.Show("没有检测到Lua脚本哦,请确保配置里面有脚本");
                return false;
            }
            if (config.Element(ns + "LaunchType") == null)
            {
                launchType = LaunchType.Positive;
                config.Add(new XElement(ns + "LaunchType", "Positive"));
            }
            if (config.Element(ns + "LaunchType").Value == "Positive")
            {
                launchType = LaunchType.Positive;
            }
            else
            {
                launchType = LaunchType.Passive;
            }
            return true;
        }

        public void RequestDispose()
        {
            isDisposed = true;
            window.Dispose();
        }
    }
}
