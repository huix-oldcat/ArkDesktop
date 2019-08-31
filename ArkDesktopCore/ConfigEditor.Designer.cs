namespace ArkDesktop
{
    partial class ConfigEditor
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
            this.listBox_LoadedPlugin = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_Launchable = new System.Windows.Forms.CheckBox();
            this.textBox_DllSHA256 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_DllPath = new System.Windows.Forms.TextBox();
            this.textBox_PluginName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label_LaunchPlugin = new System.Windows.Forms.Label();
            this.button_Rename = new System.Windows.Forms.Button();
            this.button_CopyTo = new System.Windows.Forms.Button();
            this.button_SetLaunchPlugin = new System.Windows.Forms.Button();
            this.checkBox_DefaultConfig = new System.Windows.Forms.CheckBox();
            this.textBox_ConfigName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.listBox_Config = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox_LoadedPlugin
            // 
            this.listBox_LoadedPlugin.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox_LoadedPlugin.FormattingEnabled = true;
            this.listBox_LoadedPlugin.ItemHeight = 12;
            this.listBox_LoadedPlugin.Location = new System.Drawing.Point(12, 12);
            this.listBox_LoadedPlugin.Name = "listBox_LoadedPlugin";
            this.listBox_LoadedPlugin.Size = new System.Drawing.Size(150, 136);
            this.listBox_LoadedPlugin.TabIndex = 0;
            this.listBox_LoadedPlugin.SelectedIndexChanged += new System.EventHandler(this.ListBox_LoadedPlugin_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "插件名称：";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.checkBox_Launchable);
            this.groupBox1.Controls.Add(this.textBox_DllSHA256);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox_DllPath);
            this.groupBox1.Controls.Add(this.textBox_PluginName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(168, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 159);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "插件信息";
            // 
            // checkBox_Launchable
            // 
            this.checkBox_Launchable.AutoSize = true;
            this.checkBox_Launchable.Enabled = false;
            this.checkBox_Launchable.Location = new System.Drawing.Point(7, 138);
            this.checkBox_Launchable.Name = "checkBox_Launchable";
            this.checkBox_Launchable.Size = new System.Drawing.Size(84, 16);
            this.checkBox_Launchable.TabIndex = 7;
            this.checkBox_Launchable.Text = "可启动插件";
            this.checkBox_Launchable.UseVisualStyleBackColor = true;
            // 
            // textBox_DllSHA256
            // 
            this.textBox_DllSHA256.AllowDrop = true;
            this.textBox_DllSHA256.Location = new System.Drawing.Point(6, 110);
            this.textBox_DllSHA256.Name = "textBox_DllSHA256";
            this.textBox_DllSHA256.ReadOnly = true;
            this.textBox_DllSHA256.Size = new System.Drawing.Size(188, 21);
            this.textBox_DllSHA256.TabIndex = 6;
            this.textBox_DllSHA256.DragDrop += new System.Windows.Forms.DragEventHandler(this.TextBox_DllSHA256_DragDrop);
            this.textBox_DllSHA256.DragEnter += new System.Windows.Forms.DragEventHandler(this.TextBox_DllSHA256_DragEnter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "插件所在DLL的SHA256：";
            // 
            // textBox_DllPath
            // 
            this.textBox_DllPath.Location = new System.Drawing.Point(6, 71);
            this.textBox_DllPath.Name = "textBox_DllPath";
            this.textBox_DllPath.ReadOnly = true;
            this.textBox_DllPath.Size = new System.Drawing.Size(188, 21);
            this.textBox_DllPath.TabIndex = 4;
            // 
            // textBox_PluginName
            // 
            this.textBox_PluginName.Location = new System.Drawing.Point(6, 32);
            this.textBox_PluginName.Name = "textBox_PluginName";
            this.textBox_PluginName.ReadOnly = true;
            this.textBox_PluginName.Size = new System.Drawing.Size(188, 21);
            this.textBox_PluginName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "插件所在DLL路径：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label_LaunchPlugin);
            this.groupBox2.Controls.Add(this.button_Rename);
            this.groupBox2.Controls.Add(this.button_CopyTo);
            this.groupBox2.Controls.Add(this.button_SetLaunchPlugin);
            this.groupBox2.Controls.Add(this.checkBox_DefaultConfig);
            this.groupBox2.Controls.Add(this.textBox_ConfigName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(530, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(258, 159);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "配置信息";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(130, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "删除配置";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label_LaunchPlugin
            // 
            this.label_LaunchPlugin.AutoSize = true;
            this.label_LaunchPlugin.Location = new System.Drawing.Point(6, 86);
            this.label_LaunchPlugin.Name = "label_LaunchPlugin";
            this.label_LaunchPlugin.Size = new System.Drawing.Size(41, 12);
            this.label_LaunchPlugin.TabIndex = 13;
            this.label_LaunchPlugin.Text = "label6";
            // 
            // button_Rename
            // 
            this.button_Rename.Location = new System.Drawing.Point(215, 30);
            this.button_Rename.Name = "button_Rename";
            this.button_Rename.Size = new System.Drawing.Size(37, 23);
            this.button_Rename.TabIndex = 12;
            this.button_Rename.Text = "改名";
            this.button_Rename.UseVisualStyleBackColor = true;
            this.button_Rename.Click += new System.EventHandler(this.Button_Rename_Click);
            // 
            // button_CopyTo
            // 
            this.button_CopyTo.Location = new System.Drawing.Point(6, 110);
            this.button_CopyTo.Name = "button_CopyTo";
            this.button_CopyTo.Size = new System.Drawing.Size(118, 23);
            this.button_CopyTo.TabIndex = 11;
            this.button_CopyTo.Text = "复制为新配置";
            this.button_CopyTo.UseVisualStyleBackColor = true;
            this.button_CopyTo.Click += new System.EventHandler(this.Button_CopyTo_Click);
            // 
            // button_SetLaunchPlugin
            // 
            this.button_SetLaunchPlugin.Location = new System.Drawing.Point(155, 81);
            this.button_SetLaunchPlugin.Name = "button_SetLaunchPlugin";
            this.button_SetLaunchPlugin.Size = new System.Drawing.Size(97, 23);
            this.button_SetLaunchPlugin.TabIndex = 10;
            this.button_SetLaunchPlugin.Text = "->设置启动插件";
            this.button_SetLaunchPlugin.UseVisualStyleBackColor = true;
            this.button_SetLaunchPlugin.Click += new System.EventHandler(this.Button_SetLaunchPlugin_Click);
            // 
            // checkBox_DefaultConfig
            // 
            this.checkBox_DefaultConfig.AutoSize = true;
            this.checkBox_DefaultConfig.Enabled = false;
            this.checkBox_DefaultConfig.Location = new System.Drawing.Point(6, 59);
            this.checkBox_DefaultConfig.Name = "checkBox_DefaultConfig";
            this.checkBox_DefaultConfig.Size = new System.Drawing.Size(72, 16);
            this.checkBox_DefaultConfig.TabIndex = 8;
            this.checkBox_DefaultConfig.Text = "默认配置";
            this.checkBox_DefaultConfig.UseVisualStyleBackColor = true;
            this.checkBox_DefaultConfig.CheckedChanged += new System.EventHandler(this.CheckBox_DefaultConfig_CheckedChanged);
            // 
            // textBox_ConfigName
            // 
            this.textBox_ConfigName.Location = new System.Drawing.Point(6, 32);
            this.textBox_ConfigName.Name = "textBox_ConfigName";
            this.textBox_ConfigName.Size = new System.Drawing.Size(203, 21);
            this.textBox_ConfigName.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 17);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "配置名称：";
            // 
            // listBox_Config
            // 
            this.listBox_Config.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.listBox_Config.FormattingEnabled = true;
            this.listBox_Config.ItemHeight = 12;
            this.listBox_Config.Location = new System.Drawing.Point(374, 12);
            this.listBox_Config.Name = "listBox_Config";
            this.listBox_Config.Size = new System.Drawing.Size(150, 160);
            this.listBox_Config.TabIndex = 4;
            this.listBox_Config.SelectedIndexChanged += new System.EventHandler(this.ListBox_Config_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AllowDrop = true;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(113, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "将插件拖到这里载入";
            this.label5.DragDrop += new System.Windows.Forms.DragEventHandler(this.Label5_DragDrop);
            this.label5.DragEnter += new System.Windows.Forms.DragEventHandler(this.Label5_DragEnter);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(133, 159);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(29, 12);
            this.linkLabel2.TabIndex = 6;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "警告";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel2_LinkClicked);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(84, 55);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(168, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "设为默认";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // ConfigEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 183);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.listBox_Config);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.listBox_LoadedPlugin);
            this.Name = "ConfigEditor";
            this.Text = "ConfigEditor";
            this.Load += new System.EventHandler(this.ConfigEditor_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox_LoadedPlugin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checkBox_Launchable;
        private System.Windows.Forms.TextBox textBox_DllSHA256;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_DllPath;
        private System.Windows.Forms.TextBox textBox_PluginName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_Rename;
        private System.Windows.Forms.Button button_CopyTo;
        private System.Windows.Forms.Button button_SetLaunchPlugin;
        private System.Windows.Forms.CheckBox checkBox_DefaultConfig;
        private System.Windows.Forms.TextBox textBox_ConfigName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox listBox_Config;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label_LaunchPlugin;
        private System.Windows.Forms.Button button2;
    }
}