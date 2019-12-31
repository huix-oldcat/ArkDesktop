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
using System.Xml.Linq;
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

        private void Button4_Click(object sender, EventArgs e)
        {
            configManager.ScanPlugins(Path.Combine(configManager.rootPath, "plugins"));
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            configManager.ScanConfigs(Path.Combine(configManager.rootPath, "configs"));
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            var r = (from ev in configManager.Configs where ev.ConfigName == textBox1.Text select ev);
            if (r.Any())
            {
                r.First().Description = textBox2.Text;
            }
        }
        LayeredWindowManager manager;
        XDocument document = new XDocument();

        private void Button6_Click(object sender, EventArgs e)
        {
            manager = new LayeredWindowManager(document.Root);
            manager.Parent = this;
            manager.Ready.WaitOne();
            manager.Location = new Point(200, 0);
            using (Bitmap bitmap = Bitmap.FromFile("testimg.png") as Bitmap)
            {
                manager.SetBits(bitmap);
            }
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            using (Bitmap bitmap = Bitmap.FromFile("testimg.png") as Bitmap)
            {
                manager.SetBits(bitmap);
            }
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            configManager.ScanConfigs(Path.Combine(configManager.rootPath, "configs"));
            manager = new LayeredWindowManager(new ResourceManager(Path.Combine(configManager.rootPath, "configs", "陈_站立_点击互动")));
            manager.Parent = this;
            manager.Ready.WaitOne();
            manager.Location = new Point(200, 0);
            using (Bitmap bitmap = Bitmap.FromFile("testimg.png") as Bitmap)
            {
                manager.SetBits(bitmap);
            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            manager.Dispose();
        }
    }
}
