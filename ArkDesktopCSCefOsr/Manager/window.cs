/* This Source Code Form is subject to the terms of the Mozilla Public
 * License, v. 2.0. If a copy of the MPL was not distributed with this
 * file, You can obtain one at https://mozilla.org/MPL/2.0/. */
 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    }
}
