/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
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

namespace ArkDesktopCSCefOsr
{
    public partial class ManagerForm : Form
    {
        public ManagerForm()
        {
            InitializeComponent();
        }

        private void Manager_Load(object sender, EventArgs e)
        {
            checkBox_ShowBorder.Checked = Manager.mainForm.FormBorderStyle != FormBorderStyle.None;
        }

        private void NotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.ShowInTaskbar == false)
            {
                notifyIcon.Visible = false;
                Show();
                Activate();
                WindowState = FormWindowState.Normal;
                ShowInTaskbar = true;
            }
        }

        private void ManagerWindow_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                ShowInTaskbar = false;
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(10000, "QwQ", "已经最小化了惹", ToolTipIcon.Info);
            }
        }

        private void ManagerWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            notifyIcon.Visible = true;
            Hide();
            ShowInTaskbar = false;
            notifyIcon.Dispose();
        }

        private void CheckBox_ShowBorder_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_ShowBorder.Checked)
            {
                Invoke((MethodInvoker)(() => Manager.mainForm.FormBorderStyle = FormBorderStyle.Sizable));
            }
            else
            {
                Invoke((MethodInvoker)(() => Manager.mainForm.FormBorderStyle = FormBorderStyle.None));
            }
        }

        private void Button_XY_Click(object sender, EventArgs e)
        {
            Point point = Manager.mainForm.Location;
            if (((Button)sender).Text == "X-1")
            {
                Invoke((MethodInvoker)(() =>
                {
                    point.X -= 1;
                    Manager.mainForm.Location = point;
                }));
            }
            else if (((Button)sender).Text == "X+1")
            {
                Invoke((MethodInvoker)(() =>
                {
                    point.X += 1;
                    Manager.mainForm.Location = point;
                }));
            }
            else if (((Button)sender).Text == "Y-1")
            {
                Invoke((MethodInvoker)(() =>
                {
                    point.Y -= 1;
                    Manager.mainForm.Location = point;
                }));
            }
            else
            {
                Invoke((MethodInvoker)(() =>
                {
                    point.Y += 1;
                    Manager.mainForm.Location = point;
                }));
            }
        }

        private void Button_LoadUrl_Click(object sender, EventArgs e)
        {
            Manager.LoadUrl(textBox_Location.Text);
        }

        private void Button_WH_Click(object sender, EventArgs e)
        {
            Size size = Manager.mainForm.Size;
            if (((Button)sender).Text == "W-1")
            {
                Invoke((MethodInvoker)(() =>
                {
                    size.Width -= 1;
                    Manager.mainForm.Size = size;
                }));
            }
            else if (((Button)sender).Text == "W+1")
            {
                Invoke((MethodInvoker)(() =>
                {
                    size.Width += 1;
                    Manager.mainForm.Size = size;
                }));
            }
            else if (((Button)sender).Text == "H-1")
            {
                Invoke((MethodInvoker)(() =>
                {
                    size.Height -= 1;
                    Manager.mainForm.Size = size;
                }));
            }
            else
            {
                Invoke((MethodInvoker)(() =>
                {
                    size.Height += 1;
                    Manager.mainForm.Size = size;
                }));
            }
        }

        private void Button_SaveConf_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile(Environment.CurrentDirectory + "/config.ini");
            ini.WriteInt("border", "show", checkBox_ShowBorder.Checked ? 1 : 0);
            ini.WriteInt("location", "x", Manager.mainForm.Location.X);
            ini.WriteInt("location", "y", Manager.mainForm.Location.Y);
            ini.WriteInt("size", "width", Manager.mainForm.Size.Width);
            ini.WriteInt("size", "height", Manager.mainForm.Size.Height);
            ini.WriteString("target", "url", textBox_Location.Text);
        }

        private void Button_LoadConf_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile(Environment.CurrentDirectory + "/config.ini");
            bool showBorder = ini.ReadInt("border", "show", 1) == 1;
            Point point = new Point
            {
                X = ini.ReadInt("location", "x", 300),
                Y = ini.ReadInt("location", "y", 300)
            };
            Size size = new Size
            {
                Width = ini.ReadInt("size", "width", 300),
                Height = ini.ReadInt("size", "height", 300)
            };
            Invoke((MethodInvoker)(() =>
            {
                Manager.mainForm.FormBorderStyle = showBorder ? FormBorderStyle.Sizable : FormBorderStyle.None;
                Manager.mainForm.Location = point;
                Manager.mainForm.Size = size;
            }));
            textBox_Location.Text = ini.ReadString("target", "url", textBox_Location.Text);
            if(textBox_Location.Text != "")
            {
                Button_LoadUrl_Click(button_LoadUrl, new EventArgs());
            }
        }
    }
}
