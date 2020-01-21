using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArkDesktop.CoreKit;

namespace ArkDesktop.CoreKit
{
    public partial class PluginGuiContainer : Form
    {
        private ConfigManager configManager;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ClassStyle |= 0x200;
                return createParams;
            }
        }
        public event MethodInvoker RequestClose;
        public ManualResetEvent Ready { get; private set; } = new ManualResetEvent(false);
        public PluginGuiContainer(ConfigManager configManager)
        {
            InitializeComponent();
            this.configManager = configManager;
        }
        public void BroadcastNotice(string notice)
        {
            notifyIcon.ShowBalloonTip(3000, "ArkDesktop", notice, ToolTipIcon.Info);
        }

        private void PluginGuiContainer_Load(object sender, EventArgs e)
        {
            Ready.Set();
            WindowState = FormWindowState.Minimized;
        }

        private void PluginGuiContainer_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(3000, "ArkDesktop", "最小化到托盘", ToolTipIcon.Info);
            }
            else
            {
                ShowInTaskbar = true;
                notifyIcon.Visible = false;
            }
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            //if (configManager.config.NeedSave)
            //{
            //    if (MessageBox.Show("是否保存配置？", "QAQ", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //        core.SaveConfig();
            //}
            RequestClose?.Invoke();
            Dispose();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            configManager.SaveAllConfigs();
        }

        private void Button1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                configManager.SaveAllConfigs();
            }
            RequestClose?.Invoke();
            Dispose();
        }
    }
}
