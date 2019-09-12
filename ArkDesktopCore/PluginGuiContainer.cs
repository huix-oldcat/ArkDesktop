using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkDesktop
{
    public partial class PluginGuiContainer : Form
    {
        public Core core;
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
        public bool Ready { get; private set; } = false;
        public PluginGuiContainer()
        {
            InitializeComponent();
        }

        private void PluginGuiContainer_Load(object sender, EventArgs e)
        {
            Ready = true;
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
            if(core.config.NeedSave)
            {
                if (MessageBox.Show("是否保存配置？", "QAQ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    core.SaveConfig();
            }
            RequestClose();
        }
    }
}
