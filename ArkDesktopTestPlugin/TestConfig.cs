using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkDesktop
{
    public partial class TestConfigControl : UserControl
    {
        public delegate void RequireClose();
        public RequireClose requireClose;
        public TestConfigControl()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("QwQ");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            requireClose();
        }
    }
}
