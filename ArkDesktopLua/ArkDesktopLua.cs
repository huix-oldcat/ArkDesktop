using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ArkDesktop;
using ArkDesktop.CoreKit;
using LuaInterface;
using LayeredWindow = ArkDesktop.CoreKit.LayeredWindow;
using LayeredWindowManager = ArkDesktop.CoreKit.LayeredWindowManager;

namespace ArkDesktopLua
{
    public class ArkDesktopLuaModule : IArkDesktopPluginModule
    {

        public string Name => "ArkDesktop.LuaModule";

        public Guid Guid => new Guid("b1c03350-0f81-4fee-a7ab-bacfbb13c3d4");

        public bool Disposed { get; set; } = false;

        public string Description => "ArkDesktop提供的Lua能力实现";

        public int Version => 2;

        private XElement config;

        public ManualResetEvent needDispose = new ManualResetEvent(false);
        public ManualResetEvent disposed = new ManualResetEvent(false);
        public LayeredWindowManager manager;
        internal ResourceManager resourceManager;

        private enum LaunchType
        {
            Positive, Passive
        }
        LaunchType launchType;

        public void MainThread(ResourceManager resources, InstanceHelper instanceHelper)
        {
            resourceManager = resources;
            config = resources.GetConfig(Name);
            if (EnsureConfigCorrect() == false)
            {
                resourceManager.SaveConfig(Name);
                resourceManager.SaveConfig("ArkDesktop.LayeredWindowManager");
                return;
            };
            manager = new LayeredWindowManager(resources);
            instanceHelper.AddControl("渲染窗口管理", manager);
            Lua lua = new Lua();
            LuaApi api = new LuaApi(this, lua);
            manager.window.Click += (sender, e) => api.OnClick();
            var luaThread = new Thread(new ThreadStart(() =>
            {
                while (true)
                    if (launchType == LaunchType.Positive)
                    {
                        try
                        {
                            lua.DoString(config.Element("Script").Value);
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("发生异常:" + e.Message + "\n" + e.StackTrace);
                        }
                    }
            }));
            luaThread.IsBackground = true;
            luaThread.Start();
            needDispose.WaitOne();
            luaThread.Abort();
            disposed.Set();
        }

        private bool EnsureConfigCorrect()
        {
            if (config.Element("Script") == null)
            {
                MessageBox.Show("没有检测到Lua脚本哦,请确保配置里面有脚本");
                return false;
            }
            if (config.Element("LaunchType") == null) config.Add(new XElement("LaunchType", "Positive"));
            launchType = config.Element("LaunchType").Value == "Positive" ? LaunchType.Positive : LaunchType.Passive;
            return true;
        }

        public void RequestDispose()
        {
            needDispose.Set();
            disposed.WaitOne();
        }
    }
    //public class ArkDesktopLua : IArkDesktopV2
    //{
    //    public string Name => "ArkDesktop.Lua";
    //    public string Description => "An offical plugin that provides the ability to run lua script.";
    //    public int Version => 1;

    //    public void MainThread(object coreInst)
    //    {
    //        //core = (Core)coreInst;

    //        //window = core.RequestPlugin("ArkDesktop.LayeredWindow").CreateInstance("ArkDesktop.LayeredWindow") as LayeredWindow;
    //        //manager = core.RequestPlugin("ArkDesktop.LayeredWindowManager").CreateInstance("ArkDesktop.LayeredWindowManager") as LayeredWindowManager;
    //        //manager.window = window;
    //        //manager.config = core.config;
    //        //manager.HelpPositionChange();
    //        //manager.helpZoomChange = true;
    //        //core.AddControl("渲染窗口", manager);

    //        //if (EnsureConfigCorrect() == false)
    //        //{
    //        //    return;
    //        //}

    //        //Lua lua = new Lua();
    //        //LuaApi api = new LuaApi(this, lua);
    //        //window.Click += (sender, e) => api.OnClick();

    //        //while (true)
    //        //{
    //        //    if (launchType == LaunchType.Positive)
    //        //    {
    //        //        try
    //        //        {
    //        //            lua.DoString(config.Element(ns + "LuaScript").Value);
    //        //        }
    //        //        catch (Exception e)
    //        //        {
    //        //            MessageBox.Show("发生异常:" + e.Message + "\n" + e.StackTrace);
    //        //        }
    //        //    }
    //        //    if (isDisposed)
    //        //    {
    //        //        break;
    //        //    }
    //        //}
    //    }
    //    public void RequestDispose()
    //    {
    //        isDisposed = true;
    //        window.Dispose();
    //    }
    //}
}
