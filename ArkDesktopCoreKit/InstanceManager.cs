using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkDesktop.CoreKit
{
    public class InstanceHelper
    {
        private readonly string extraName;

        public PluginGuiContainer PluginGuiContainer { get; private set; }
        public InstanceHelper(PluginGuiContainer container, string extraName)
        {
            PluginGuiContainer = container;
            this.extraName = extraName;
        }

        public void AddControl(string controlName, Control control)
        {
            if (PluginGuiContainer.tabControl.InvokeRequired)
            {
                PluginGuiContainer.tabControl.Invoke((MethodInvoker)(() => AddControl(controlName, control)));
                return;
            }
            controlName = string.Format("[{0}]{1}", extraName, controlName);
            if (PluginGuiContainer.tabControl.TabPages.ContainsKey(controlName))
            {
                throw new Exception("Control name exists.");
            }
            TabPage tabPage = new TabPage(controlName);
            tabPage.Controls.Add(control);
            PluginGuiContainer.tabControl.TabPages.Add(tabPage);
        }

        public void RemoveControl(string controlName)
        {
            if (PluginGuiContainer.tabControl.InvokeRequired)
            {
                PluginGuiContainer.tabControl.Invoke((MethodInvoker)(() => RemoveControl(controlName)));
            }
            controlName = string.Format("[{0}]{1}", extraName, controlName);
            if (PluginGuiContainer.tabControl.TabPages.ContainsKey(controlName))
            {
                PluginGuiContainer.tabControl.TabPages.Remove(PluginGuiContainer.tabControl.TabPages[PluginGuiContainer.tabControl.TabPages.IndexOfKey(controlName)]);
            }
        }
    }
        
    public class InstanceManager
    {
        public PluginGuiContainer pluginGuiContainer;
        public ManualResetEvent Ready;
        public Thread thread;

        public InstanceManager(PluginGuiContainer pluginGuiContainer)
        {
            this.pluginGuiContainer = pluginGuiContainer;
            Ready = pluginGuiContainer.Ready;
            thread = new Thread(new ThreadStart(() =>
            {
                Application.EnableVisualStyles();
                Application.Run(pluginGuiContainer);
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }

        public void LaunchModule(ConfigInfo info)
        {
            IArkDesktopPluginModule module = info.LaunchModule.assembly.CreateInstance(info.LaunchModule.fullName) as IArkDesktopPluginModule;
            pluginGuiContainer.RequestClose += module.RequestDispose;
            Ready.WaitOne();
            module.MainThread(info.ResourceManager, new InstanceHelper(pluginGuiContainer, info.ConfigName));
        }
    }
}
