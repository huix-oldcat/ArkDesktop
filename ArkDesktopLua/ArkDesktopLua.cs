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
using Neo.IronLua;
using LayeredWindow = ArkDesktop.CoreKit.LayeredWindow;
using LayeredWindowManager = ArkDesktop.CoreKit.LayeredWindowManager;

namespace ArkDesktopLua
{
    public class ArkDesktopLuaModule : IArkDesktopPluginModule
    {
        public readonly string[] featureList;

        public string Name => "ArkDesktop.LuaModule";

        public Guid Guid => new Guid("b1c03350-0f81-4fee-a7ab-bacfbb13c3d4");

        public bool Disposed { get; set; } = false;

        public string Description => "ArkDesktop提供的Lua能力实现";

        public int Version => 2;

        public bool CheckFeature(string featureName) => featureList.Contains(featureName);

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

        private enum LoadPosition
        {
            InConfig, OutFile
        }
        LoadPosition loadPosition;

        private string luaScript;

        public ArkDesktopLuaModule()
        {
            featureList = new string[]
                {
                    "LAUNCH_PositiveLaunch",
                    "LAUNCH_PassiveLaunch",
                    "API_LoadBitmap",
                    "API_DisplayBitmap",
                    "API_Sleep",
                    "API_RequestClickEvent",
                    "API_CreateDraft",
                    "API_CopyBitmapToDraft",
                    "API_DrawDraft",
                    "API_MoveWindow(Alpha)",
                    "API_SetFlag",
                    "API_PlaySound",
                    "FLAG_autoClearBackground",
                    "FLAG_reverse",
                    "FLAG_strictMode"
                };
        }

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
            instanceHelper.AddControl("", manager);
            if (loadPosition == LoadPosition.InConfig)
            {

                if (config.Element("Script") == null)
                {
                    MessageBox.Show("似乎没有加载到脚本,请联系配置包制作者");
                    return;
                }
                luaScript = config.Element("Script").Value;
            }
            else
            {
                using (var sr = new System.IO.StreamReader(resourceManager.OpenRead("script.lua"))) luaScript = sr.ReadToEnd();
                if (luaScript == "")
                {
                    MessageBox.Show("似乎没有加载到脚本,请联系配置包制作者", "QAQ");
                    return;
                }
            }
            using (Lua lua = new Lua())
            {
                dynamic env = lua.CreateEnvironment();
                LuaApi api = new LuaApi(this, lua, env);
                var chunk = lua.CompileChunk(luaScript, "script.lua", new LuaCompileOptions { DebugEngine = LuaStackTraceDebugger.Default });
                manager.window.Click += (sender, e) => api.OnClick();
                var luaThread = new Thread(new ThreadStart(() =>
                {
                    if (launchType == LaunchType.Positive)
                    {
                        while (true)
                            try
                            {
                                env.dochunk(chunk);
                            }
                            catch (ThreadAbortException)
                            {
                                break;
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("发生异常:" + e.Message + "\n" + e.StackTrace);
                            }
                    }
                    else
                    {
                        int st = 0;
                        LuaChunk initChunk = null, updateChunk = null;
                        while (true)
                        {
                            try
                            {
                                if (st == 0)
                                {
                                    env.dochunk(chunk);
                                    initChunk = lua.CompileChunk("init()", "script.lua", new LuaCompileOptions());
                                    updateChunk = lua.CompileChunk("update()", "script.lua", new LuaCompileOptions());
                                    st = 1;
                                }
                                if (st == 1)
                                {
                                    env.dochunk(initChunk);
                                    st = 2;
                                }
                                if (st == 2)
                                {
                                    var begin = DateTime.Now;
                                    double duration = env.update();
                                    var used = DateTime.Now - begin;
                                    var need = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(duration * 1000));
                                    if (need > used) Thread.Sleep(need - used);
                                }
                            }
                            catch (ThreadAbortException)
                            {
                                break;
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show("发生异常:" + e.Message + "\n" + e.StackTrace);
                            }
                        }
                    }
                }));
                luaThread.IsBackground = true;
                luaThread.Start();
                needDispose.WaitOne();
                luaThread.Abort();
                disposed.Set();
            }
        }

        private bool EnsureConfigCorrect()
        {
            if (config.Element("LaunchType") == null) config.Add(new XElement("LaunchType", "Positive"));
            if (config.Element("LoadPosition") == null) config.Add(new XElement("LoadPosition", "InConfig"));
            launchType = config.Element("LaunchType").Value == "Positive" ? LaunchType.Positive : LaunchType.Passive;
            loadPosition = config.Element("LoadPosition").Value == "OutFile" ? LoadPosition.OutFile : LoadPosition.InConfig;
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
