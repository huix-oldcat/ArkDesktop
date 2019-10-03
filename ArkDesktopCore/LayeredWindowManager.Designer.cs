namespace ArkDesktop
{
    partial class LayeredWindowManager
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Pos = new System.Windows.Forms.Button();
            this.linkLabel_Zoom = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel_ZoomQuality = new System.Windows.Forms.LinkLabel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.checkBox_ShowTaskbarIcon = new System.Windows.Forms.CheckBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // button_Pos
            // 
            this.button_Pos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Pos.Location = new System.Drawing.Point(3, 3);
            this.button_Pos.Name = "button_Pos";
            this.button_Pos.Size = new System.Drawing.Size(101, 23);
            this.button_Pos.TabIndex = 0;
            this.button_Pos.Text = "窗口位置";
            this.button_Pos.UseVisualStyleBackColor = true;
            this.button_Pos.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.button_Pos.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_Pos_MouseMove);
            this.button_Pos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // linkLabel_Zoom
            // 
            this.linkLabel_Zoom.AutoSize = true;
            this.linkLabel_Zoom.Location = new System.Drawing.Point(44, 29);
            this.linkLabel_Zoom.Name = "linkLabel_Zoom";
            this.linkLabel_Zoom.Size = new System.Drawing.Size(29, 12);
            this.linkLabel_Zoom.TabIndex = 3;
            this.linkLabel_Zoom.TabStop = true;
            this.linkLabel_Zoom.Text = "x1.0";
            this.linkLabel_Zoom.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_Zoom_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "缩放:";
            // 
            // linkLabel_ZoomQuality
            // 
            this.linkLabel_ZoomQuality.AutoSize = true;
            this.linkLabel_ZoomQuality.Location = new System.Drawing.Point(79, 29);
            this.linkLabel_ZoomQuality.Name = "linkLabel_ZoomQuality";
            this.linkLabel_ZoomQuality.Size = new System.Drawing.Size(41, 12);
            this.linkLabel_ZoomQuality.TabIndex = 5;
            this.linkLabel_ZoomQuality.TabStop = true;
            this.linkLabel_ZoomQuality.Text = "高品质";
            this.linkLabel_ZoomQuality.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel_ZoomQuality_LinkClicked);
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "默认",
            "置于桌面",
            "置顶（强力）"});
            this.comboBox1.Location = new System.Drawing.Point(3, 44);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(84, 20);
            this.comboBox1.TabIndex = 6;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(110, 8);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(11, 12);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "?";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // checkBox_ShowTaskbarIcon
            // 
            this.checkBox_ShowTaskbarIcon.AutoSize = true;
            this.checkBox_ShowTaskbarIcon.Location = new System.Drawing.Point(4, 71);
            this.checkBox_ShowTaskbarIcon.Name = "checkBox_ShowTaskbarIcon";
            this.checkBox_ShowTaskbarIcon.Size = new System.Drawing.Size(96, 16);
            this.checkBox_ShowTaskbarIcon.TabIndex = 8;
            this.checkBox_ShowTaskbarIcon.Text = "不显示任务栏";
            this.checkBox_ShowTaskbarIcon.UseVisualStyleBackColor = true;
            this.checkBox_ShowTaskbarIcon.CheckedChanged += new System.EventHandler(this.CheckBox_ShowTaskbarIcon_CheckedChanged);
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(110, 47);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(11, 12);
            this.linkLabel2.TabIndex = 9;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "?";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel2_LinkClicked);
            // 
            // linkLabel3
            // 
            this.linkLabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(93, 47);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(11, 12);
            this.linkLabel3.TabIndex = 10;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "!";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel3_LinkClicked);
            // 
            // LayeredWindowManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel3);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.checkBox_ShowTaskbarIcon);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.linkLabel_ZoomQuality);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel_Zoom);
            this.Controls.Add(this.button_Pos);
            this.Name = "LayeredWindowManager";
            this.Size = new System.Drawing.Size(124, 100);
            this.Load += new System.EventHandler(this.LayeredWindowManager_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Pos;
        private System.Windows.Forms.LinkLabel linkLabel_Zoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel_ZoomQuality;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox checkBox_ShowTaskbarIcon;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel3;
    }
}
