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
using System.Xml.Linq;
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
            core = new Core(AppDomain.CurrentDomain.BaseDirectory);
            //Test2();
            //Test3();
            Test4();
        }

        private void Test1()
        {
            core.SaveConfig();
            core.ImportPlugin("ArkDesktopTestPlugin.dll");
            core.CreateInst(core.GetLoadedPlugins()[0]);
        }

        private void Test2()
        {
            core.SaveConfig();
            core.ImportPlugin("ArkDesktopStaticPic.dll");
            core.CreateInst(core.GetLoadedPlugins()[0]);
        }

        private void Test3()
        {
            Thread thread = new Thread(new ThreadStart(() => Application.Run(new ConfigEditor() { core = core }))); ;
            thread.IsBackground = true;
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        private void Test4()
        {
            core.config.GetElement(((XNamespace)"ArkDesktop") + "StaticPic").Name = "_LAUNCH";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            core.SaveConfig();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!core.config.NeedSave)
            {
                return;
            }
            if (e.CloseReason == CloseReason.UserClosing)
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
