/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.Win32;

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
            IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "/config.ini");
            if (ini.ReadInt("auto", "load", 0) == 1)
            {
                while (Manager.startupFinished == false) ;
                LoadConfig();
            }
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
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = false;
                Manager.cfxThread.Abort();
                Manager.layeredWindow.Invoke((MethodInvoker)(() => Manager.layeredWindow.Dispose()));
            }
            e.Cancel = true;
            notifyIcon.Visible = true;
            Hide();
            ShowInTaskbar = false;
            notifyIcon.Dispose();
        }

        private void Button_XY_Click(object sender, EventArgs e)
        {
            Point point = Manager.layeredWindow.Location;
            if (((Button)sender).Text == "X-1")
            {
                Invoke((MethodInvoker)(() =>
                {
                    point.X -= 1;
                    Manager.layeredWindow.Location = point;
                }));
            }
            else if (((Button)sender).Text == "X+1")
            {
                Invoke((MethodInvoker)(() =>
                {
                    point.X += 1;
                    Manager.layeredWindow.Location = point;
                }));
            }
            else if (((Button)sender).Text == "Y-1")
            {
                Invoke((MethodInvoker)(() =>
                {
                    point.Y -= 1;
                    Manager.layeredWindow.Location = point;
                }));
            }
            else
            {
                Invoke((MethodInvoker)(() =>
                {
                    point.Y += 1;
                    Manager.layeredWindow.Location = point;
                }));
            }
        }

        private void Button_LoadUrl_Click(object sender, EventArgs e)
        {
            Manager.LoadUrl(textBox_Location.Text);
        }

        private void Button_WH_Click(object sender, EventArgs e)
        {
            Size size = Manager.layeredWindow.Size;
            if (((Button)sender).Text == "W-1")
            {
                Invoke((MethodInvoker)(() =>
                {
                    size.Width -= 1;
                    Manager.layeredWindow.Size = size;
                }));
            }
            else if (((Button)sender).Text == "W+1")
            {
                Invoke((MethodInvoker)(() =>
                {
                    size.Width += 1;
                    Manager.layeredWindow.Size = size;
                }));
            }
            else if (((Button)sender).Text == "H-1")
            {
                Invoke((MethodInvoker)(() =>
                {
                    size.Height -= 1;
                    Manager.layeredWindow.Size = size;
                }));
            }
            else
            {
                Invoke((MethodInvoker)(() =>
                {
                    size.Height += 1;
                    Manager.layeredWindow.Size = size;
                }));
            }

            Manager.control.OnResize();
        }

        private void Button_SaveConf_Click(object sender, EventArgs e)
        {
            IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "/config.ini");
            ini.WriteInt("location", "x", Manager.layeredWindow.Location.X);
            ini.WriteInt("location", "y", Manager.layeredWindow.Location.Y);
            ini.WriteInt("size", "width", Manager.layeredWindow.Size.Width);
            ini.WriteInt("size", "height", Manager.layeredWindow.Size.Height);
            ini.WriteString("target", "url", textBox_Location.Text);
            ini.WriteString("zoom", "zoomlevel", Manager.control.Zoom.ToString());
            ini.WriteInt("auto", "load", checkBox_AutoLoad.Checked ? 1 : 0);
        }

        private void Button_LoadConf_Click(object sender, EventArgs e)
        {
            LoadConfig();
        }

        public void LoadConfig()
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker)(() => LoadConfig()));
                return;
            }
            IniFile ini = new IniFile(AppDomain.CurrentDomain.BaseDirectory + "/config.ini");
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
            Manager.layeredWindow.Invoke((MethodInvoker)(() =>
            {
                Manager.layeredWindow.Location = point;
                Manager.layeredWindow.Size = size;
            }));
            textBox_Location.Text = ini.ReadString("target", "url", textBox_Location.Text);
            if (textBox_Location.Text != "")
            {
                Button_LoadUrl_Click(button_LoadUrl, new EventArgs());
            }
            linkLabel_Zoom.Text = "缩放:" + ini.ReadString("zoom", "zoomlevel", "1");
            Manager.control.Zoom = Convert.ToDouble(ini.ReadString("zoom", "zoomlevel", "1"));
            checkBox_AutoLoad.Checked = ini.ReadInt("auto", "load", 0) == 1;
        }

        private void Button_TryFindProgman_Click(object sender, EventArgs e)
        {
            textBox_AttachHwnd.Text = Manager.GetHandleString(Manager.FindProgman());
        }

        private void Button_ApplyAttach_Click(object sender, EventArgs e)
        {
            if (textBox_AttachHwnd.Text == "")
            {
                return;
            }
            Manager.SetMainFormParent(Manager.GetHandleFromString(textBox_AttachHwnd.Text));
        }

        private bool isMouseDown = false;
        private Point lastPosition;
        private void Button_ChangePos_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            lastPosition = e.Location;
            Manager.layeredWindow.Invoke((MethodInvoker)(() =>
            {
                Manager.layeredWindow.drawBorder = true;
                Manager.control.OnResize();
            }));
        }

        private void Button_ChangePos_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            Manager.layeredWindow.Invoke((MethodInvoker)(() =>
            {
                Manager.layeredWindow.drawBorder = false;
                Manager.control.OnResize();
            }));
        }

        private void Button_ChangePos_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == false)
            {
                return;
            }
            int deltaX = e.X - lastPosition.X;
            int deltaY = e.Y - lastPosition.Y;
            lastPosition = e.Location;
            Manager.layeredWindow.Invoke((MethodInvoker)(() =>
            {
                Manager.layeredWindow.Location = new Point(Manager.layeredWindow.Location.X + deltaX,
                                                           Manager.layeredWindow.Location.Y + deltaY);
            }));
        }

        private void Button_ChangeSize_MouseDown(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            lastPosition = e.Location;
            Manager.layeredWindow.Invoke((MethodInvoker)(() =>
            {
                Manager.layeredWindow.drawBorder = true;
                Manager.control.OnResize();
            }));
        }

        private void Button_ChangeSize_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            Manager.layeredWindow.Invoke((MethodInvoker)(() =>
            {
                Manager.layeredWindow.drawBorder = false;
                Manager.control.OnResize();
            }));
        }

        private void Button_ChangeSize_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMouseDown == false)
            {
                return;
            }
            int deltaX = e.X - lastPosition.X;
            int deltaY = e.Y - lastPosition.Y;
            lastPosition = e.Location;
            Manager.layeredWindow.Invoke((MethodInvoker)(() =>
            {
                Manager.layeredWindow.Size = new Size(Manager.layeredWindow.Size.Width + deltaX,
                                                      Manager.layeredWindow.Size.Height + deltaY);
                Manager.control.OnResize();
            }));
        }

        private void linkLabel_Zoom_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                Manager.control.Zoom = Convert.ToDouble(Interaction.InputBox("输入新的缩放级别(小数)", "输入", Manager.control.Zoom.ToString()));
                linkLabel_Zoom.Text = "缩放:" + Manager.control.Zoom.ToString();
            }
            catch (FormatException)
            {
                Manager.control.Zoom = 1d;
            }
        }

        private void Button_SetAutoRun_Click(object sender, EventArgs e)
        {
            RegistryKey autorunKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);
            autorunKey.SetValue("ArkDesktop", Application.ExecutablePath);
            autorunKey.Close();
        }

        private void Button_ResetAutoRun_Click(object sender, EventArgs e)
        {
            RegistryKey autorunKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);
            autorunKey.DeleteValue("ArkDesktop", false);
            autorunKey.Close();
        }

        private void Button_CheckAutoRun_Click(object sender, EventArgs e)
        {
            RegistryKey autorunKey = Registry.CurrentUser.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\\", true);
            if (autorunKey.GetValue("ArkDesktop") == null)
            {
                MessageBox.Show("没有的呢", "查询结果", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (autorunKey.GetValue("ArkDesktop").ToString() != Application.ExecutablePath)
            {
                MessageBox.Show("存在一个名为ArkDesktop的启动项,但没有指向本可执行文件,而是指向了:\n" + autorunKey.GetValue("ArkDesktop").ToString(), "查询结果", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
