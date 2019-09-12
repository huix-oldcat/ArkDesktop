/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace ArkDesktop
{
    public class Core
    {
        /// <summary>
        ///全局配置提供者
        /// </summary>
        public Config config;

        /// <summary>
        /// Container窗口所在线程ID
        /// </summary>
        public int ContainerThreadID { get; private set; }

        /// <summary>
        /// 寻找配置、插件等所基于的根目录
        /// </summary>
        public string RootPath { get; private set; }

        private Dictionary<string, IArkDesktopPlugin> plugins = new Dictionary<string, IArkDesktopPlugin>();
        private List<Thread> threads = new List<Thread>();
        private Thread containerThread;
        private PluginGuiContainer container;
        private Dictionary<string, string> pluginFrom = new Dictionary<string, string>();
        private Dictionary<string, bool> launchable = new Dictionary<string, bool>();
        private List<ManualResetEvent> waits = new List<ManualResetEvent>();

        /// <summary>
        /// ArkDesktop Core构造类
        /// 应由ArkDesktop Service等程序调用
        /// </summary>
        /// <param name="rootPath">寻找配置、插件等所基于的根目录</param>
        /// <param name="loadPlugins">是否加载默认配置</param>
        public Core(string rootPath, bool loadPlugins = true)
        {
            RootPath = rootPath;
            config = new Config();
            if (File.Exists(Path.Combine(rootPath, "Config.xml")))
            {
                using (FileStream fs = File.OpenRead(Path.Combine(rootPath, "Config.xml")))
                {
                    config.Load(fs);
                }
            }
            else
            {
                config.Create();
            }
            container = new PluginGuiContainer();
            container.core = this;
            container.RequestClose += CloseThreads;
            containerThread = new Thread(new ThreadStart(() => Application.Run(container)));
            containerThread.IsBackground = true;
            containerThread.SetApartmentState(ApartmentState.STA);
            containerThread.Start();
            ContainerThreadID = containerThread.ManagedThreadId;
            if (loadPlugins)
            {
                foreach (string i in config.PluginList)
                {
                    ImportPlugin(i);
                }
            }
        }

        public void CloseThreads()
        {
            foreach(Thread thread in threads)
            {
                thread.Abort();
            }
            foreach(ManualResetEvent resetEvent in waits)
            {
                resetEvent.Set();
            }
        }

        public void MainThread()
        {
            if (waits.Any())
            {
                WaitHandle.WaitAll(waits.ToArray());
            }
        }

        /// <summary>
        /// 导入一个插件
        /// 插件应位于/Plugins目录
        /// </summary>
        /// <param name="name">插件文件名称，含后缀名</param>
        public void ImportPlugin(string name)
        {
            if (!File.Exists(Path.Combine(RootPath, "Plugins", name)))
            {
                throw new Exception("File doesn't exist. (You should copy it to /Plugins directory)");
            }
            Assembly assembly = Assembly.LoadFrom(Path.Combine(RootPath, "Plugins", name));
            var found = from e in assembly.GetTypes()
                        where typeof(IArkDesktopPlugin).IsAssignableFrom(e)
                        select e;
            if (!found.Any())
            {
                throw new Exception("Not an ArkDesktop plugin");
            }
            foreach (Type type in found)
            {
                IArkDesktopPlugin newPlugin = (IArkDesktopPlugin)type.GetConstructor(new Type[0]).Invoke(null);
                if (plugins.ContainsKey(newPlugin.Name))
                {
                    continue;
                }
                plugins.Add(newPlugin.Name, newPlugin);
                pluginFrom.Add(newPlugin.Name, name);
                launchable.Add(newPlugin.Name, typeof(IArkDesktopLaunchable).IsAssignableFrom(type));
            }
            config.AddPlugin(name);
        }

        /// <summary>
        /// 获取已导入的插件列表
        /// </summary>
        /// <returns>导入了的插件列表</returns>
        public List<string> GetLoadedPlugins()
        {
            return plugins.Keys.ToList();
        }

        public string GetDllNameByPluginName(string pluginName)
        {
            return pluginFrom.ContainsKey(pluginName) ? pluginFrom[pluginName] : null;
        }

        public bool IsLaunchable(string pluginName)
        {
            return launchable.ContainsKey(pluginName) ? launchable[pluginName] : false;
        }

        public void SaveConfig()
        {
            using (FileStream fs = File.Open(Path.Combine(RootPath, "Config.xml"), FileMode.Create))
            {
                config.Save(fs);
            }
        }
        public void SaveBackupConfig()
        {
            using (FileStream fs = File.Open(Path.Combine(RootPath, "Config.xml"), FileMode.Create))
            {
                config.Save(fs);
            }
        }

        /// <summary>
        /// 新建线程来创建插件实例
        /// 应由Arkdesktop Service，PluginGuiContainer或默认Config的加载者调用
        /// </summary>
        /// <param name="pluginName">插件名称，为实现了<c>IArkDesktop</c>的类的Name访问器的值</param>
        public void CreateInst(string pluginName)
        {
            if (!plugins.ContainsKey(pluginName))
            {
                throw new Exception("Plugin hasn't been loaded.");
            }
            if (launchable[pluginName] == false)
            {
                throw new Exception("Plugin is not launchable.");
            }
            ManualResetEvent mre = new ManualResetEvent(false);
            waits.Add(mre);
            Thread thread = new Thread(new ParameterizedThreadStart((object o) =>
            {
                ((IArkDesktopLaunchable)plugins[pluginName]).MainThread(o);
                mre.Set();
            }));
            thread.IsBackground = true;
            thread.Start(this);
            threads.Add(thread);
        }

        /// <summary>
        /// 请求一个插件的程序集
        /// 内建的插件列表:
        /// <list type="table">
        ///     <listheader>
        ///         <term><paramref name="pluginName"/></term>
        ///         <description>插件功能</description>
        ///     </listheader>
        ///     <item>
        ///         <term>ArkDesktop.LayeredWindow</term>
        ///         <description>LayeredWindow，使用<c>SetBits</c>加载位图</description>
        ///     </item>
        ///     <item>
        ///         <description>ArkDesktop.LayeredWindowManager</description>
        ///         <description>为用户提供简单的<c>LayeredWindow</c>操作，如移动窗口等</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <param name="pluginName">插件名称，为实现了<c>IArkDesktop</c>的类的Name访问器的值。</param>
        /// <returns></returns>
        public Assembly RequestPlugin(string pluginName)
        {
            if (pluginName == "ArkDesktop.LayeredWindow")
            {
                return typeof(LayeredWindow).Assembly;
            }
            if (pluginName == "ArkDesktop.LayeredWindowManager")
            {
                return typeof(LayeredWindowManager).Assembly;
            }
            if (!plugins.ContainsKey(pluginName))
            {
                throw new Exception("Plugin hasn't been loaded.");
            }
            return plugins[pluginName].GetType().Assembly;
        }

        /// <summary>
        /// 在<c>Container</c>线程中加载一个控件
        /// </summary>
        /// <param name="tabName">Tab页名称，不可重复</param>
        /// <param name="control">加载的控件</param>
        public void AddControl(string tabName, UserControl control)
        {
            if (container.InvokeRequired)
            {
                InvokeContainer(() => AddControl(tabName, control));
                return;
            }
            if (container.tabControl.TabPages.ContainsKey(tabName))
            {
                return;
            }
            if (container.tabControl.InvokeRequired)
            {
                container.tabControl.Invoke((MethodInvoker)(() => AddControl(tabName, control)));
                return;
            }
            TabPage tabPage = new TabPage(tabName);
            tabPage.Controls.Add(control);
            container.tabControl.TabPages.Add(tabPage);
            return;
        }

        /// <summary>
        /// 卸载一个控件
        /// </summary>
        /// <param name="tabName">Tab页名称</param>
        public void RemoveControl(string tabName)
        {
            if (FindTabPage(tabName) == null)
            {
                return;
            }
            if (container.tabControl.InvokeRequired)
            {
                container.Invoke((MethodInvoker)(() => RemoveControl(tabName)));
                return;
            }
            container.tabControl.TabPages.Remove(FindTabPage(tabName));
        }

        /// <summary>
        /// 查询Container是否包含<paramref name="tabName"/>
        /// </summary>
        /// <param name="tabName">Tab页名称</param>
        /// <returns></returns>
        public bool ContainsControl(string tabName)
        {
            return FindTabPage(tabName) != null;
        }

        private TabPage FindTabPage(string tabName)
        {
            foreach (TabPage tabPage in container.tabControl.TabPages)
            {
                if (tabPage.Text == tabName)
                {
                    return tabPage;
                }
            }
            return null;
        }

        /// <summary>
        /// 在<c>Container</c>线程执行委托
        /// </summary>
        /// <param name="invoker">委托</param>
        public void InvokeContainer(MethodInvoker invoker)
        {
            container.Invoke(invoker);
        }
    }
}
