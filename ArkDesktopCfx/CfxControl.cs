using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkDesktopCfx
{
    public partial class CfxControl : UserControl
    {
        private ArkDesktopCfx master;
        public CfxControl(ArkDesktopCfx m)
        {
            master = m;
            InitializeComponent();
        }

        private void CfxControl_Load(object sender, EventArgs e)
        {
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            master.manager.browser.MainFrame.LoadUrl(textBox1.Text);
        }

        private void Button_ChangeWindowSize_Click(object sender, EventArgs e)
        {
            master.window.Invoke((MethodInvoker)(() => {
                master.window.Width = int.Parse(textBox_W.Text);
                master.window.Height = int.Parse(textBox_H.Text);
            }));
        }
    }
}
