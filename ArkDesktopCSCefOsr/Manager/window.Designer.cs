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
            this.button_PosY = new System.Windows.Forms.Button();
            this.button_NegY = new System.Windows.Forms.Button();
            this.button_PosX = new System.Windows.Forms.Button();
            this.button_NegX = new System.Windows.Forms.Button();
            this.checkBox_ShowBorder = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_LoadScript = new System.Windows.Forms.Button();
            this.button_LoadUrl = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
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
            this.groupBox1.Size = new System.Drawing.Size(108, 107);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "窗口管理";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.button_PosY, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_NegY, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.button_PosX, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.button_NegX, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 42);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(96, 59);
            this.tableLayoutPanel1.TabIndex = 4;
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
            // checkBox_ShowBorder
            // 
            this.checkBox_ShowBorder.AutoSize = true;
            this.checkBox_ShowBorder.Location = new System.Drawing.Point(6, 20);
            this.checkBox_ShowBorder.Name = "checkBox_ShowBorder";
            this.checkBox_ShowBorder.Size = new System.Drawing.Size(96, 16);
            this.checkBox_ShowBorder.TabIndex = 3;
            this.checkBox_ShowBorder.Text = "显示窗口边框";
            this.checkBox_ShowBorder.UseVisualStyleBackColor = true;
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
            // ManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 129);
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
    }
}