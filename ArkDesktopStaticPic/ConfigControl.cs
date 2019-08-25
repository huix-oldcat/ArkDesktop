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
    public partial class ConfigControl : UserControl
    {
        public ArkDesktopStaticPic makerParent;
        public LayeredWindowManager manager;
        public ConfigControl()
        {
            InitializeComponent();
        }

        private void ConfigControl_DragDrop(object sender, DragEventArgs e)
        {
            string[] data = (string[])e.Data.GetData(DataFormats.FileDrop);
            List<string> list = new List<string>();
            List<Bitmap> bitmaps = new List<Bitmap>();
            foreach(string path in data)
            {
                Bitmap newBitmap = (Bitmap)Image.FromFile(path);
                if(newBitmap == null)
                {
                    continue;
                }
                list.Add(path);
                bitmaps.Add(newBitmap);
            }
            makerParent.RequestModifyBitmaps(bitmaps);
            makerParent.SetFrameList(list.ToArray());
        }

        private void ConfigControl_DragEnter(object sender, DragEventArgs e)
        {
            if(e.Data.GetData(DataFormats.FileDrop) != null)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("首先你要将文件按播放顺序排好序（一般是修改文件名称为1~99之类的）\n然后选中所有图片\n接下来按住图片的**第一张**拖动到本窗口");
        }

        private void ConfigControl_Load(object sender, EventArgs e)
        {
            manager.Dock = DockStyle.Fill;
            tableLayoutPanel1.Controls.Add(manager, 1, 0);
        }
    }
}
