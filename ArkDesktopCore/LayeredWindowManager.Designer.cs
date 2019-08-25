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
            this.button_Zoom = new System.Windows.Forms.Button();
            this.button_Attach = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_Pos
            // 
            this.button_Pos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Pos.Location = new System.Drawing.Point(3, 3);
            this.button_Pos.Name = "button_Pos";
            this.button_Pos.Size = new System.Drawing.Size(95, 23);
            this.button_Pos.TabIndex = 0;
            this.button_Pos.Text = "窗口位置";
            this.button_Pos.UseVisualStyleBackColor = true;
            this.button_Pos.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Button_MouseDown);
            this.button_Pos.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Button_Pos_MouseMove);
            this.button_Pos.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Button_MouseUp);
            // 
            // button_Zoom
            // 
            this.button_Zoom.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Zoom.Location = new System.Drawing.Point(3, 32);
            this.button_Zoom.Name = "button_Zoom";
            this.button_Zoom.Size = new System.Drawing.Size(95, 24);
            this.button_Zoom.TabIndex = 1;
            this.button_Zoom.Text = "窗口缩放";
            this.button_Zoom.UseVisualStyleBackColor = true;
            // 
            // button_Attach
            // 
            this.button_Attach.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Attach.Location = new System.Drawing.Point(3, 62);
            this.button_Attach.Name = "button_Attach";
            this.button_Attach.Size = new System.Drawing.Size(95, 24);
            this.button_Attach.TabIndex = 2;
            this.button_Attach.Text = "附加到桌面";
            this.button_Attach.UseVisualStyleBackColor = true;
            this.button_Attach.Click += new System.EventHandler(this.Button_Attach_Click);
            // 
            // LayeredWindowManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_Attach);
            this.Controls.Add(this.button_Zoom);
            this.Controls.Add(this.button_Pos);
            this.Name = "LayeredWindowManager";
            this.Size = new System.Drawing.Size(101, 93);
            this.Load += new System.EventHandler(this.LayeredWindowManager_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Pos;
        private System.Windows.Forms.Button button_Zoom;
        private System.Windows.Forms.Button button_Attach;
    }
}
