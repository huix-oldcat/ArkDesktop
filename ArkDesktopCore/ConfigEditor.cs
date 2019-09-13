using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArkDesktop
{
    public partial class ConfigEditor : Form
    {
        public Core core;

        class PluginItem
        {
            public PluginItem(string name, string dllPath, string dllSHA256, bool launchable)
            {
                Name = name;
                DllPath = dllPath;
                DllSHA256 = dllSHA256;
                Launchable = launchable;
            }

            public string Name { get; private set; }
            public string DllPath { get; private set; }
            public string DllSHA256 { get; private set; }
            public bool Launchable { get; private set; }
            public override string ToString() => Name;
        }
        public ConfigEditor()
        {
            InitializeComponent();
        }

        private void ListBox_LoadedPlugin_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePlugin();
        }

        private void ClearPluginInfo()
        {
            textBox_PluginName.Text = "";
            textBox_DllPath.Text = "";
            textBox_DllSHA256.Text = "";
            checkBox_Launchable.Checked = false;
        }

        private void UpdatePlugin()
        {
            PluginItem item = (PluginItem)listBox_LoadedPlugin.SelectedItem;
            if (item == null)
            {
                ClearPluginInfo();
                return;
            }
            textBox_PluginName.Text = item.Name;
            textBox_DllPath.Text = item.DllPath;
            textBox_DllSHA256.Text = item.DllSHA256;
            checkBox_Launchable.Checked = item.Launchable;
        }

        private void Button_LoadPlugin_Click(object sender, EventArgs e)
        {

        }

        private void Label5_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Label5_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            if (Path.GetDirectoryName(path) != Path.Combine(core.RootPath, "Plugins"))
            {
                File.Copy(path, Path.Combine(core.RootPath, "Plugins/", Path.GetFileName(path)));
            }
            core.ImportPlugin(Path.GetFileName(path));
            LoadPluginList();
        }

        private string CalcSHA256(string filePath)
        {
            string retval;
            using (FileStream fs = File.OpenRead(filePath))
            {
                byte[] vs = new byte[fs.Length];
                int bytesRead = 0, bytesToRead = (int)fs.Length;
                while (bytesToRead > 0)
                {
                    int read = fs.Read(vs, bytesRead, bytesToRead);
                    bytesToRead -= read;
                    bytesRead += read;
                }
                byte[] hash = SHA256.Create().ComputeHash(vs);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; ++i)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                retval = sb.ToString();
            }
            return retval;
        }

        private void LoadPluginList()
        {
            listBox_LoadedPlugin.Items.Clear();
            foreach (string name in core.GetLoadedPlugins())
            {
                string dllName = core.GetDllNameByPluginName(name);
                string dllSHA256 = CalcSHA256(Path.Combine(core.RootPath, "Plugins/", dllName));
                listBox_LoadedPlugin.Items.Add(new PluginItem(name, dllName, dllSHA256, core.IsLaunchable(name)));
            }
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("因加载DLL会执行其中的代码，所以请不要载入未知来源的DLL\n推荐下载开源项目的插件！\n你也可以把插件拖到哈希值显示框来安全计算文件哈希", "QAQ!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void TextBox_DllSHA256_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void TextBox_DllSHA256_DragDrop(object sender, DragEventArgs e)
        {
            ClearPluginInfo();
            textBox_DllPath.Text = ((string[])e.Data.GetData(DataFormats.FileDrop))[0];
            textBox_DllSHA256.Text = CalcSHA256(textBox_DllPath.Text);
        }

        private void LoadConfigList()
        {
            listBox_Config.Items.Clear();
            foreach (var i in core.config.ConfigList)
            {
                listBox_Config.Items.Add(i.configName);
            }
        }

        private void ListBox_Config_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateConfig();
        }

        private void UpdateConfig()
        {
            if (listBox_Config.SelectedItem == null)
            {
                return;
            }
            string configName = listBox_Config.SelectedItem.ToString();
            core.config.ChangeActiveConfig(configName);
            textBox_ConfigName.Text = configName;
            label_LaunchPlugin.Text = core.config.GetLaunchPlugin();
            checkBox_DefaultConfig.Checked = core.config.IsDefaultConfig();
        }

        private void ConfigEditor_Load(object sender, EventArgs e)
        {
            LoadConfigList();
            LoadPluginList();
        }

        private void Button_Rename_Click(object sender, EventArgs e)
        {
            try
            {
                core.config.RenameConfig(textBox_ConfigName.Text);
            }
            catch (Exception ex)
            {
                if (ex.Message != "Config exists.")
                {
                    throw ex;
                }
                MessageBox.Show("配置名称已经存在！");
            }
            LoadConfigList();
            listBox_Config.SelectedIndex = listBox_Config.Items.IndexOf(textBox_ConfigName.Text);
            UpdateConfig();
        }

        private void CheckBox_DefaultConfig_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            core.config.ChangeDefaultConfig(listBox_Config.SelectedItem.ToString());
        }

        private void Button_SetLaunchPlugin_Click(object sender, EventArgs e)
        {
            if (listBox_LoadedPlugin.SelectedItem != null && ((PluginItem)listBox_LoadedPlugin.SelectedItem).Launchable)
            {
                core.config.SetLaunchPlugin(((PluginItem)listBox_LoadedPlugin.SelectedItem).Name);
                UpdateConfig();
            }
        }

        private void Button_CopyTo_Click(object sender, EventArgs e)
        {
            string newName = Interaction.InputBox("输入新配置的名称", "QwQ");
            if (newName != "")
            {
                try
                {
                    core.config.CopyTo(newName);
                }
                catch (Exception ex)
                {
                    if (ex.Message != "Dest config exists.")
                    {
                        throw ex;
                    }
                    MessageBox.Show("配置已经存在", "QAQ");
                }
                finally
                {
                    LoadConfigList();
                    listBox_Config.SelectedIndex = listBox_Config.Items.IndexOf(textBox_ConfigName.Text);
                    UpdateConfig();
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (core.config.ConfigList.Length < 2)
            {
                MessageBox.Show("至少要保留一个配置啊", "QAQ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            core.config.RemoveConfig();
            LoadConfigList();
            listBox_Config.SelectedIndex = listBox_Config.Items.IndexOf((from i in core.config.ConfigList where i.isDefault select i.configName).FirstOrDefault());
            UpdateConfig();
        }

        private void ConfigEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!core.config.NeedSave)
            {
                return;
            }
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("是否保存配置？", "QAQ", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    core.SaveConfig();
            }
            else
            {
                core.SaveBackupConfig();
            }
        }
    }
}
