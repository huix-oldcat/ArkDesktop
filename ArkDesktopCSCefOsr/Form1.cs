using Chromium;
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

namespace ArkDesktopCSCefOsr
{
    public partial class Form1 : Form
    {
        Thread thread;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            thread = new Thread(new ThreadStart(CfxRuntime.RunMessageLoop));
            thread.IsBackground = true;
            thread.Start();
        }
    }
}
