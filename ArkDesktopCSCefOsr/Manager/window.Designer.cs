namespace ArkDesktopCSCefOsr
{
    partial class ManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManagerForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.textBox_Location = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_PosX = new System.Windows.Forms.Button();
            this.button_NegX = new System.Windows.Forms.Button();
            this.button_NegY = new System.Windows.Forms.Button();
            this.button_PosY = new System.Windows.Forms.Button();
            this.button_NegW = new System.Windows.Forms.Button();
            this.button_PosW = new System.Windows.Forms.Button();
            this.button_NegH = new System.Windows.Forms.Button();
            this.button_PosH = new System.Windows.Forms.Button();
            this.button_ChangePos = new System.Windows.Forms.Button();
            this.button_ChangeSize = new System.Windows.Forms.Button();
            this.linkLabel_Zoom = new System.Windows.Forms.LinkLabel();
            this.button_LoadScript = new System.Windows.Forms.Button();
            this.button_LoadUrl = new System.Windows.Forms.Button();
            this.button_LoadConf = new System.Windows.Forms.Button();
            this.button_SaveConf = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.button_ApplyAttach = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_AttachHwnd = new System.Windows.Forms.TextBox();
            this.button_TryFindProgman = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.checkBox_AutoLoad = new System.Windows.Forms.CheckBox();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.button_ResetAutoRun = new System.Windows.Forms.Button();
            this.button_CheckAutoRun = new System.Windows.Forms.Button();
            this.button_SetAutoRun = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "notifyIcon1";
            this.notifyIcon.Visible = true;
            this.notifyIcon.DoubleClick += new System.EventHandler(this.NotifyIcon_DoubleClick);
            // 
            // textBox_Location
            // 
            this.textBox_Location.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Location.Location = new System.Drawing.Point(8, 5);
            this.textBox_Location.Margin = new System.Windows.Forms.Padding(4, 1, 4, 4);
            this.textBox_Location.Name = "textBox_Location";
            this.textBox_Location.Size = new System.Drawing.Size(356, 25);
            this.textBox_Location.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.button_PosX, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_NegX, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_NegY, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_PosY, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_NegW, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_PosW, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_NegH, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_PosH, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_ChangePos, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button_ChangeSize, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.linkLabel_Zoom, 2, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(368, 115);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // button_PosX
            // 
            this.button_PosX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_PosX.Location = new System.Drawing.Point(96, 4);
            this.button_PosX.Margin = new System.Windows.Forms.Padding(4);
            this.button_PosX.Name = "button_PosX";
            this.button_PosX.Size = new System.Drawing.Size(84, 30);
            this.button_PosX.TabIndex = 2;
            this.button_PosX.Text = "X+1";
            this.button_PosX.UseVisualStyleBackColor = true;
            this.button_PosX.Click += new System.EventHandler(this.Button_XY_Click);
            // 
            // button_NegX
            // 
            this.button_NegX.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_NegX.Location = new System.Drawing.Point(4, 4);
            this.button_NegX.Margin = new System.Windows.Forms.Padding(4);
            this.button_NegX.Name = "button_NegX";
            this.button_NegX.Size = new System.Drawing.Size(84, 30);
            this.button_NegX.TabIndex = 1;
            this.button_NegX.Text = "X-1";
            this.button_NegX.UseVisualStyleBackColor = true;
            this.button_NegX.Click += new System.EventHandler(this.Button_XY_Click);
            // 
            // button_NegY
            // 
            this.button_NegY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_NegY.Location = new System.Drawing.Point(188, 4);
            this.button_NegY.Margin = new System.Windows.Forms.Padding(4);
            this.button_NegY.Name = "button_NegY";
            this.button_NegY.Size = new System.Drawing.Size(84, 30);
            this.button_NegY.TabIndex = 3;
            this.button_NegY.Text = "Y-1";
            this.button_NegY.UseVisualStyleBackColor = true;
            this.button_NegY.Click += new System.EventHandler(this.Button_XY_Click);
            // 
            // button_PosY
            // 
            this.button_PosY.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_PosY.Location = new System.Drawing.Point(280, 4);
            this.button_PosY.Margin = new System.Windows.Forms.Padding(4);
            this.button_PosY.Name = "button_PosY";
            this.button_PosY.Size = new System.Drawing.Size(84, 30);
            this.button_PosY.TabIndex = 4;
            this.button_PosY.Text = "Y+1";
            this.button_PosY.UseVisualStyleBackColor = true;
            this.button_PosY.Click += new System.EventHandler(this.Button_XY_Click);
            // 
            // button_NegW
            // 
            this.button_NegW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_NegW.Location = new System.Drawing.Point(4, 42);
            this.button_NegW.Margin = new System.Windows.Forms.Padding(4);
            this.button_NegW.Name = "button_NegW";
            this.button_NegW.Size = new System.Drawing.Size(84, 30);
            this.button_NegW.TabIndex = 5;
            this.button_NegW.Text = "W-1";
            this.button_NegW.UseVisualStyleBackColor = true;
            this.button_NegW.Click += new System.EventHandler(this.Button_WH_Click);
            // 
            // button_PosW
            // 
            this.button_PosW.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_PosW.Location = new System.Drawing.Point(96, 42);
            this.button_PosW.Margin = new System.Windows.Forms.Padding(4);
            this.button_PosW.Name = "button_PosW";
            this.button_PosW.Size = new System.Drawing.Size(84, 30);
            this.button_PosW.TabIndex = 6;
            this.button_PosW.Text = "W+1";
            this.button_PosW.UseVisualStyleBackColor = true;
            this.button_PosW.Click += new System.EventHandler(this.Button_WH_Click);
            // 
            // button_NegH
            // 
            this.button_NegH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_NegH.Location = new System.Drawing.Point(188, 42);
            this.button_NegH.Margin = new System.Windows.Forms.Padding(4);
            this.button_NegH.Name = "button_NegH";
            this.button_NegH.Size = new System.Drawing.Size(84, 30);
            this.button_NegH.TabIndex = 7;
            this.button_NegH.Text = "H-1";
            this.button_NegH.UseVisualStyleBackColor = true;
            this.button_NegH.Click += new System.EventHandler(this.Button_WH_Click);
            // 
            // button_PosH
            // 
            this.button_PosH.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_PosH.Location = new System.Drawing.Point(280, 42);
            this.button_PosH.Margin = new System.Windows.Forms.Padding(4);
            this.button_PosH.Name = "button_PosH";
            this.button_PosH.Size = new System.Drawing.Size(84, 30);
            this.button_PosH.TabIndex = 6;
            this.button_PosH.Text = "H+1";
            this.button_PosH.UseVisualStyleBackColor = true;
            this.button_PosH.Click += new System.EventHandler(this.Button_WH_Click);
            // 
            // button_ChangePos
            // 
            this.button_ChangePos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ChangePos.Location = new System.Drawing.Point(4, 80);
            this.button_ChangePos.Margin = new System.Windows.Forms.Padding(4);
            this.button_ChangePos.Name = "button_ChangePos";
            this.button_ChangePos.Size = new System.Drawing.Size(84, 31);
            this.button_ChangePos.TabIndex = 9;
            this.button_ChangePos.Text = "Pos";
            this.button_ChangePos.UseVisualStyleBackColor = true;
            this.button_ChangePos.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_ChangePos_MouseDown);
            this.button_ChangePos.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_ChangePos_MouseMove);
            this.button_ChangePos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_ChangePos_MouseUp);
            // 
            // button_ChangeSize
            // 
            this.button_ChangeSize.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button_ChangeSize.Location = new System.Drawing.Point(96, 80);
            this.button_ChangeSize.Margin = new System.Windows.Forms.Padding(4);
            this.button_ChangeSize.Name = "button_ChangeSize";
            this.button_ChangeSize.Size = new System.Drawing.Size(84, 31);
            this.button_ChangeSize.TabIndex = 8;
            this.button_ChangeSize.Text = "Size";
            this.button_ChangeSize.UseVisualStyleBackColor = true;
            this.button_ChangeSize.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_ChangeSize_MouseDown);
            this.button_ChangeSize.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_ChangeSize_MouseMove);
            this.button_ChangeSize.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_ChangeSize_MouseUp);
            // 
            // linkLabel_Zoom
            // 
            this.linkLabel_Zoom.AutoSize = true;
            this.linkLabel_Zoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.linkLabel_Zoom.Location = new System.Drawing.Point(188, 76);
            this.linkLabel_Zoom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel_Zoom.Name = "linkLabel_Zoom";
            this.linkLabel_Zoom.Size = new System.Drawing.Size(84, 39);
            this.linkLabel_Zoom.TabIndex = 10;
            this.linkLabel_Zoom.TabStop = true;
            this.linkLabel_Zoom.Text = "缩放:1";
            this.linkLabel_Zoom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.linkLabel_Zoom.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_Zoom_LinkClicked);
            // 
            // button_LoadScript
            // 
            this.button_LoadScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LoadScript.Enabled = false;
            this.button_LoadScript.Location = new System.Drawing.Point(8, 75);
            this.button_LoadScript.Margin = new System.Windows.Forms.Padding(4);
            this.button_LoadScript.Name = "button_LoadScript";
            this.button_LoadScript.Size = new System.Drawing.Size(357, 29);
            this.button_LoadScript.TabIndex = 5;
            this.button_LoadScript.Text = "加载本地akd脚本";
            this.button_LoadScript.UseVisualStyleBackColor = true;
            // 
            // button_LoadUrl
            // 
            this.button_LoadUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LoadUrl.Location = new System.Drawing.Point(8, 39);
            this.button_LoadUrl.Margin = new System.Windows.Forms.Padding(4);
            this.button_LoadUrl.Name = "button_LoadUrl";
            this.button_LoadUrl.Size = new System.Drawing.Size(357, 29);
            this.button_LoadUrl.TabIndex = 4;
            this.button_LoadUrl.Text = "加载远程URL";
            this.button_LoadUrl.UseVisualStyleBackColor = true;
            this.button_LoadUrl.Click += new System.EventHandler(this.Button_LoadUrl_Click);
            // 
            // button_LoadConf
            // 
            this.button_LoadConf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_LoadConf.Location = new System.Drawing.Point(8, 46);
            this.button_LoadConf.Margin = new System.Windows.Forms.Padding(4);
            this.button_LoadConf.Name = "button_LoadConf";
            this.button_LoadConf.Size = new System.Drawing.Size(357, 31);
            this.button_LoadConf.TabIndex = 2;
            this.button_LoadConf.Text = "加载配置";
            this.button_LoadConf.UseVisualStyleBackColor = true;
            this.button_LoadConf.Click += new System.EventHandler(this.Button_LoadConf_Click);
            // 
            // button_SaveConf
            // 
            this.button_SaveConf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SaveConf.Location = new System.Drawing.Point(8, 8);
            this.button_SaveConf.Margin = new System.Windows.Forms.Padding(4);
            this.button_SaveConf.Name = "button_SaveConf";
            this.button_SaveConf.Size = new System.Drawing.Size(357, 31);
            this.button_SaveConf.TabIndex = 1;
            this.button_SaveConf.Text = "保存当前窗口为默认配置";
            this.button_SaveConf.UseVisualStyleBackColor = true;
            this.button_SaveConf.Click += new System.EventHandler(this.Button_SaveConf_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(16, 15);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(384, 152);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(376, 123);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "渲染窗口";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button_LoadScript);
            this.tabPage2.Controls.Add(this.textBox_Location);
            this.tabPage2.Controls.Add(this.button_LoadUrl);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(376, 123);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "数据";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.linkLabel1);
            this.tabPage3.Controls.Add(this.button_ApplyAttach);
            this.tabPage3.Controls.Add(this.label1);
            this.tabPage3.Controls.Add(this.textBox_AttachHwnd);
            this.tabPage3.Controls.Add(this.button_TryFindProgman);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(376, 123);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "位置";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(346, 11);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(15, 15);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "?";
            // 
            // button_ApplyAttach
            // 
            this.button_ApplyAttach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ApplyAttach.Location = new System.Drawing.Point(8, 78);
            this.button_ApplyAttach.Margin = new System.Windows.Forms.Padding(4);
            this.button_ApplyAttach.Name = "button_ApplyAttach";
            this.button_ApplyAttach.Size = new System.Drawing.Size(357, 29);
            this.button_ApplyAttach.TabIndex = 13;
            this.button_ApplyAttach.Text = "修改主窗口的Parent";
            this.button_ApplyAttach.UseVisualStyleBackColor = true;
            this.button_ApplyAttach.Click += new System.EventHandler(this.Button_ApplyAttach_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "句柄:";
            // 
            // textBox_AttachHwnd
            // 
            this.textBox_AttachHwnd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_AttachHwnd.Location = new System.Drawing.Point(63, 8);
            this.textBox_AttachHwnd.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_AttachHwnd.Name = "textBox_AttachHwnd";
            this.textBox_AttachHwnd.Size = new System.Drawing.Size(274, 25);
            this.textBox_AttachHwnd.TabIndex = 10;
            // 
            // button_TryFindProgman
            // 
            this.button_TryFindProgman.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_TryFindProgman.Location = new System.Drawing.Point(8, 41);
            this.button_TryFindProgman.Margin = new System.Windows.Forms.Padding(4);
            this.button_TryFindProgman.Name = "button_TryFindProgman";
            this.button_TryFindProgman.Size = new System.Drawing.Size(357, 29);
            this.button_TryFindProgman.TabIndex = 11;
            this.button_TryFindProgman.Text = "尝试寻找可用句柄(桌面底层)";
            this.button_TryFindProgman.UseVisualStyleBackColor = true;
            this.button_TryFindProgman.Click += new System.EventHandler(this.Button_TryFindProgman_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.checkBox_AutoLoad);
            this.tabPage4.Controls.Add(this.button_LoadConf);
            this.tabPage4.Controls.Add(this.button_SaveConf);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage4.Size = new System.Drawing.Size(376, 123);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "配置";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // checkBox_AutoLoad
            // 
            this.checkBox_AutoLoad.AutoSize = true;
            this.checkBox_AutoLoad.Location = new System.Drawing.Point(9, 86);
            this.checkBox_AutoLoad.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_AutoLoad.Name = "checkBox_AutoLoad";
            this.checkBox_AutoLoad.Size = new System.Drawing.Size(255, 19);
            this.checkBox_AutoLoad.TabIndex = 3;
            this.checkBox_AutoLoad.Text = "自动加载默认配置(请修改后保存)";
            this.checkBox_AutoLoad.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.button_SetAutoRun);
            this.tabPage5.Controls.Add(this.button_CheckAutoRun);
            this.tabPage5.Controls.Add(this.button_ResetAutoRun);
            this.tabPage5.Location = new System.Drawing.Point(4, 25);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(376, 123);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "自启";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // button_ResetAutoRun
            // 
            this.button_ResetAutoRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ResetAutoRun.Location = new System.Drawing.Point(7, 46);
            this.button_ResetAutoRun.Margin = new System.Windows.Forms.Padding(4);
            this.button_ResetAutoRun.Name = "button_ResetAutoRun";
            this.button_ResetAutoRun.Size = new System.Drawing.Size(362, 31);
            this.button_ResetAutoRun.TabIndex = 2;
            this.button_ResetAutoRun.Text = "删除自启";
            this.button_ResetAutoRun.UseVisualStyleBackColor = true;
            this.button_ResetAutoRun.Click += new System.EventHandler(this.Button_ResetAutoRun_Click);
            // 
            // button_CheckAutoRun
            // 
            this.button_CheckAutoRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_CheckAutoRun.Location = new System.Drawing.Point(7, 85);
            this.button_CheckAutoRun.Margin = new System.Windows.Forms.Padding(4);
            this.button_CheckAutoRun.Name = "button_CheckAutoRun";
            this.button_CheckAutoRun.Size = new System.Drawing.Size(362, 31);
            this.button_CheckAutoRun.TabIndex = 3;
            this.button_CheckAutoRun.Text = "查询自启";
            this.button_CheckAutoRun.UseVisualStyleBackColor = true;
            this.button_CheckAutoRun.Click += new System.EventHandler(this.Button_CheckAutoRun_Click);
            // 
            // button_SetAutoRun
            // 
            this.button_SetAutoRun.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SetAutoRun.Location = new System.Drawing.Point(7, 7);
            this.button_SetAutoRun.Margin = new System.Windows.Forms.Padding(4);
            this.button_SetAutoRun.Name = "button_SetAutoRun";
            this.button_SetAutoRun.Size = new System.Drawing.Size(362, 31);
            this.button_SetAutoRun.TabIndex = 4;
            this.button_SetAutoRun.Text = "添加自启";
            this.button_SetAutoRun.UseVisualStyleBackColor = true;
            this.button_SetAutoRun.Click += new System.EventHandler(this.Button_SetAutoRun_Click);
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 182);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(434, 229);
            this.Name = "ManagerForm";
            this.Text = "manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManagerWindow_FormClosing);
            this.Load += new System.EventHandler(this.Manager_Load);
            this.Resize += new System.EventHandler(this.ManagerWindow_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.TextBox textBox_Location;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_PosY;
        private System.Windows.Forms.Button button_NegY;
        private System.Windows.Forms.Button button_PosX;
        private System.Windows.Forms.Button button_NegX;
        private System.Windows.Forms.Button button_LoadScript;
        private System.Windows.Forms.Button button_LoadUrl;
        private System.Windows.Forms.Button button_NegW;
        private System.Windows.Forms.Button button_NegH;
        private System.Windows.Forms.Button button_PosH;
        private System.Windows.Forms.Button button_PosW;
        private System.Windows.Forms.Button button_LoadConf;
        private System.Windows.Forms.Button button_SaveConf;
        private System.Windows.Forms.Button button_ChangePos;
        private System.Windows.Forms.Button button_ChangeSize;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button button_ApplyAttach;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_AttachHwnd;
        private System.Windows.Forms.Button button_TryFindProgman;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.LinkLabel linkLabel_Zoom;
        private System.Windows.Forms.CheckBox checkBox_AutoLoad;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Button button_SetAutoRun;
        private System.Windows.Forms.Button button_CheckAutoRun;
        private System.Windows.Forms.Button button_ResetAutoRun;
    }
}