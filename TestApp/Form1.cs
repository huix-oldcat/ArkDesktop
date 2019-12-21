using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ArkDesktop.CoreKit;

namespace TestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        ConfigManager configManager;

        private void Form1_Load(object sender, EventArgs e)
        {
            configManager = new ConfigManager(AppDomain.CurrentDomain.BaseDirectory);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            configManager.LoadDll(Path.Combine(configManager.rootPath, "plugins", "ArkDesktopLua.dll"));
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            configManager.LoadConfig(Path.Combine(configManager.rootPath, "configs", "陈_站立_点击互动"));
        }
    }
}
