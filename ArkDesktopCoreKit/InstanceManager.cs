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
        public PluginGuiContainer PluginGuiContainer { get; private set; }
        public InstanceHelper(PluginGuiContainer container)
        {
            PluginGuiContainer = container;
        }

        public void AddControl(string controlName, Control control)
        {
            if (PluginGuiContainer.tabControl.InvokeRequired)
            {
                PluginGuiContainer.tabControl.Invoke((MethodInvoker)(() => AddControl(controlName, control)));
                return;
            }
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
            if (PluginGuiContainer.tabControl.TabPages.ContainsKey(controlName))
            {
                PluginGuiContainer.tabControl.TabPages.Remove(PluginGuiContainer.tabControl.TabPages[PluginGuiContainer.tabControl.TabPages.IndexOfKey(controlName)]);
            }
        }
    }

    public class InstanceManager
    {
        public InstanceHelper instanceHelper;
        public ManualResetEvent Ready;
        public Thread thread;

        public InstanceManager(InstanceHelper instanceHelper)
        {
            this.instanceHelper = instanceHelper;
            Ready = instanceHelper.PluginGuiContainer.Ready;
        }

        public void Start()
        {
            thread = new Thread(new ThreadStart(() =>
            {
                Application.EnableVisualStyles();
                Application.Run(instanceHelper.PluginGuiContainer);
            }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
        }

        public void LaunchModule(ConfigInfo info)
        {
            IArkDesktopPluginModule module = info.LaunchModule.assembly.CreateInstance(info.LaunchModule.fullName) as IArkDesktopPluginModule;
            instanceHelper.PluginGuiContainer.RequestClose += module.RequestDispose;
            Ready.WaitOne();
            module.MainThread(info.ResourceManager, instanceHelper);
        }
    }
}
