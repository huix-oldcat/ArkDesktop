using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArkDesktop;

namespace ArkDesktopTest
{
    public partial class Form1 : Form
    {
        Core core;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //core = new Core(AppDomain.CurrentDomain.BaseDirectory);
            Test2();
        }

        private void Test1()
        {
            core = new Core(AppDomain.CurrentDomain.BaseDirectory);
            core.SaveConfig();
            core.ImportPlugin("ArkDesktopTestPlugin.dll");
            core.CreateInst(core.GetLoadedPlugins()[0]);
        }

        private void Test2()
        {
            core = new Core(AppDomain.CurrentDomain.BaseDirectory);
            core.SaveConfig();
            core.ImportPlugin("ArkDesktopStaticPic.dll");
            core.CreateInst(core.GetLoadedPlugins()[0]);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            core.SaveConfig();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!core.config.NeedSave)
            {
                return;
            }
            if(e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("是否保存配置？", "QAQ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    core.SaveConfig();
            }
            else
            {
                core.SaveBackupConfig();
            }
        }
    }
}
