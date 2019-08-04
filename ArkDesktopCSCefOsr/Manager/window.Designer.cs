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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.button_NegH = new System.Windows.Forms.Button();
            this.button_PosH = new System.Windows.Forms.Button();
            this.button_PosY = new System.Windows.Forms.Button();
            this.button_NegY = new System.Windows.Forms.Button();
            this.button_PosX = new System.Windows.Forms.Button();
            this.button_NegX = new System.Windows.Forms.Button();
            this.button_NegW = new System.Windows.Forms.Button();
            this.button_PosW = new System.Windows.Forms.Button();
            this.checkBox_ShowBorder = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_LoadScript = new System.Windows.Forms.Button();
            this.button_LoadUrl = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.button_LoadConf = new System.Windows.Forms.Button();
            this.button_SaveConf = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
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
            this.textBox_Location.Location = new System.Drawing.Point(6, 18);
            this.textBox_Location.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
            this.textBox_Location.Name = "textBox_Location";
            this.textBox_Location.Size = new System.Drawing.Size(322, 21);
            this.textBox_Location.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Controls.Add(this.checkBox_ShowBorder);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(108, 165);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "窗口管理";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.button_NegH, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.button_PosH, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.button_PosY, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_NegY, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_PosX, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_NegX, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_NegW, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.button_PosW, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 42);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(96, 117);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // button_NegH
            // 
            this.button_NegH.Location = new System.Drawing.Point(3, 90);
            this.button_NegH.Name = "button_NegH";
            this.button_NegH.Size = new System.Drawing.Size(42, 23);
            this.button_NegH.TabIndex = 7;
            this.button_NegH.Text = "H-1";
            this.button_NegH.UseVisualStyleBackColor = true;
            this.button_NegH.Click += new System.EventHandler(this.Button_WH_Click);
            // 
            // button_PosH
            // 
            this.button_PosH.Location = new System.Drawing.Point(51, 90);
            this.button_PosH.Name = "button_PosH";
            this.button_PosH.Size = new System.Drawing.Size(42, 23);
            this.button_PosH.TabIndex = 6;
            this.button_PosH.Text = "H+1";
            this.button_PosH.UseVisualStyleBackColor = true;
            this.button_PosH.Click += new System.EventHandler(this.Button_WH_Click);
            // 
            // button_PosY
            // 
            this.button_PosY.Location = new System.Drawing.Point(51, 32);
            this.button_PosY.Name = "button_PosY";
            this.button_PosY.Size = new System.Drawing.Size(42, 23);
            this.button_PosY.TabIndex = 4;
            this.button_PosY.Text = "Y+1";
            this.button_PosY.UseVisualStyleBackColor = true;
            // 
            // button_NegY
            // 
            this.button_NegY.Location = new System.Drawing.Point(3, 32);
            this.button_NegY.Name = "button_NegY";
            this.button_NegY.Size = new System.Drawing.Size(42, 23);
            this.button_NegY.TabIndex = 3;
            this.button_NegY.Text = "Y-1";
            this.button_NegY.UseVisualStyleBackColor = true;
            // 
            // button_PosX
            // 
            this.button_PosX.Location = new System.Drawing.Point(51, 3);
            this.button_PosX.Name = "button_PosX";
            this.button_PosX.Size = new System.Drawing.Size(42, 23);
            this.button_PosX.TabIndex = 2;
            this.button_PosX.Text = "X+1";
            this.button_PosX.UseVisualStyleBackColor = true;
            // 
            // button_NegX
            // 
            this.button_NegX.Location = new System.Drawing.Point(3, 3);
            this.button_NegX.Name = "button_NegX";
            this.button_NegX.Size = new System.Drawing.Size(42, 23);
            this.button_NegX.TabIndex = 1;
            this.button_NegX.Text = "X-1";
            this.button_NegX.UseVisualStyleBackColor = true;
            // 
            // button_NegW
            // 
            this.button_NegW.Location = new System.Drawing.Point(3, 61);
            this.button_NegW.Name = "button_NegW";
            this.button_NegW.Size = new System.Drawing.Size(42, 23);
            this.button_NegW.TabIndex = 5;
            this.button_NegW.Text = "W-1";
            this.button_NegW.UseVisualStyleBackColor = true;
            this.button_NegW.Click += new System.EventHandler(this.Button_WH_Click);
            // 
            // button_PosW
            // 
            this.button_PosW.Location = new System.Drawing.Point(51, 61);
            this.button_PosW.Name = "button_PosW";
            this.button_PosW.Size = new System.Drawing.Size(42, 23);
            this.button_PosW.TabIndex = 6;
            this.button_PosW.Text = "W+1";
            this.button_PosW.UseVisualStyleBackColor = true;
            this.button_PosW.Click += new System.EventHandler(this.Button_WH_Click);
            // 
            // checkBox_ShowBorder
            // 
            this.checkBox_ShowBorder.AutoSize = true;
            this.checkBox_ShowBorder.Location = new System.Drawing.Point(6, 20);
            this.checkBox_ShowBorder.Name = "checkBox_ShowBorder";
            this.checkBox_ShowBorder.Size = new System.Drawing.Size(96, 16);
            this.checkBox_ShowBorder.TabIndex = 3;
            this.checkBox_ShowBorder.Text = "显示窗口边框";
            this.checkBox_ShowBorder.UseVisualStyleBackColor = true;
            this.checkBox_ShowBorder.CheckedChanged += new System.EventHandler(this.CheckBox_ShowBorder_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button_LoadScript);
            this.groupBox2.Controls.Add(this.button_LoadUrl);
            this.groupBox2.Controls.Add(this.textBox_Location);
            this.groupBox2.Location = new System.Drawing.Point(126, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(334, 107);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "地址管理";
            // 
            // button_LoadScript
            // 
            this.button_LoadScript.Enabled = false;
            this.button_LoadScript.Location = new System.Drawing.Point(7, 75);
            this.button_LoadScript.Name = "button_LoadScript";
            this.button_LoadScript.Size = new System.Drawing.Size(321, 23);
            this.button_LoadScript.TabIndex = 5;
            this.button_LoadScript.Text = "加载本地akd脚本";
            this.button_LoadScript.UseVisualStyleBackColor = true;
            // 
            // button_LoadUrl
            // 
            this.button_LoadUrl.Location = new System.Drawing.Point(7, 46);
            this.button_LoadUrl.Name = "button_LoadUrl";
            this.button_LoadUrl.Size = new System.Drawing.Size(321, 23);
            this.button_LoadUrl.TabIndex = 4;
            this.button_LoadUrl.Text = "加载远程URL";
            this.button_LoadUrl.UseVisualStyleBackColor = true;
            this.button_LoadUrl.Click += new System.EventHandler(this.Button_LoadUrl_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tableLayoutPanel2);
            this.groupBox3.Location = new System.Drawing.Point(127, 126);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(333, 51);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "配置文件";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.button_LoadConf, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.button_SaveConf, 0, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(7, 18);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(320, 27);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // button_LoadConf
            // 
            this.button_LoadConf.Location = new System.Drawing.Point(163, 3);
            this.button_LoadConf.Name = "button_LoadConf";
            this.button_LoadConf.Size = new System.Drawing.Size(154, 21);
            this.button_LoadConf.TabIndex = 2;
            this.button_LoadConf.Text = "加载配置";
            this.button_LoadConf.UseVisualStyleBackColor = true;
            this.button_LoadConf.Click += new System.EventHandler(this.Button_LoadConf_Click);
            // 
            // button_SaveConf
            // 
            this.button_SaveConf.Location = new System.Drawing.Point(3, 3);
            this.button_SaveConf.Name = "button_SaveConf";
            this.button_SaveConf.Size = new System.Drawing.Size(154, 21);
            this.button_SaveConf.TabIndex = 1;
            this.button_SaveConf.Text = "保存当前窗口为默认配置";
            this.button_SaveConf.UseVisualStyleBackColor = true;
            this.button_SaveConf.Click += new System.EventHandler(this.Button_SaveConf_Click);
            // 
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 183);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "ManagerForm";
            this.Text = "manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ManagerWindow_FormClosing);
            this.Load += new System.EventHandler(this.Manager_Load);
            this.Resize += new System.EventHandler(this.ManagerWindow_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.TextBox textBox_Location;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button button_PosY;
        private System.Windows.Forms.Button button_NegY;
        private System.Windows.Forms.Button button_PosX;
        private System.Windows.Forms.Button button_NegX;
        private System.Windows.Forms.CheckBox checkBox_ShowBorder;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_LoadScript;
        private System.Windows.Forms.Button button_LoadUrl;
        private System.Windows.Forms.Button button_NegW;
        private System.Windows.Forms.Button button_NegH;
        private System.Windows.Forms.Button button_PosH;
        private System.Windows.Forms.Button button_PosW;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button button_LoadConf;
        private System.Windows.Forms.Button button_SaveConf;
    }
}