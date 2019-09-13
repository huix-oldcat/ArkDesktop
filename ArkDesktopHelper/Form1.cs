using Microsoft.Win32;
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

namespace ArkDesktopHelper
{
    public partial class Form1 : Form
    {
        Core core;
        public Form1(Core coreInst)
        {
            core = coreInst;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
            checkBox1.Checked = key.GetValue("ArkDesktopHelper")?.ToString() == AppDomain.CurrentDomain.BaseDirectory + " -autorun";
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (checkBox1.Checked)
            {
                key.SetValue("ArkDesktopHelper", AppDomain.CurrentDomain.BaseDirectory + " -autorun");
            }
            else
            {
                key.DeleteValue("ArkDesktopHelper", false);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach(Config.ConfigInfo i in core.config.ConfigList)
            {
                if(i.isDefault)
                {
                    comboBox1.Items.Insert(0, i.configName);
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    comboBox1.Items.Add(i.configName);
                }
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            using (ConfigEditor editor = new ConfigEditor())
            {
                editor.core = core;
                editor.ShowDialog();
            }
        }
    }
}
