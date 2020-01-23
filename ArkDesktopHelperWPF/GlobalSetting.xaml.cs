using ArkDesktop.CoreKit;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArkDesktopHelperWPF
{
    /// <summary>
    /// GlobalSetting.xaml 的交互逻辑
    /// </summary>
    public partial class GlobalSetting : UserControl
    {
        private readonly MainWindow.RequestConfigList requestConfigList;
        private readonly ConfigManager manager;

        public GlobalSetting(MainWindow.RequestConfigList requestConfigList, ArkDesktop.CoreKit.ConfigManager manager)
        {
            InitializeComponent();
            this.requestConfigList = requestConfigList;
            this.manager = manager;
            if (File.Exists("AutoRun.txt"))
            {
                AutoRunListTextBox.Text = "";
                using (var fs = File.OpenRead("AutoRun.txt"))
                using (var sr = new StreamReader(fs))
                    while (sr.EndOfStream == false)
                        try
                        {
                            string s = sr.ReadLine();
                            var g = Guid.Parse(s);
                            AutoRunListTextBox.Text += g.ToString().Substring(24) + ' ';
                            var f = from i in manager.Configs where i.ConfigGuid == g select i.ConfigName;
                            if (f.Any()) AutoRunListTextBox.Text += f.First();
                            else AutoRunListTextBox.Text += "(未找到)";
                            AutoRunListTextBox.Text += '\n';
                        }
                        catch (Exception) { }
            }
        }

        private void AutoRunCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            if (AutoRunCheckbox.IsChecked == true)
            {
                var list = requestConfigList?.Invoke();
                if (list == null || list.Any() == false)
                {
                    AutoRunListTextBox.Text = "没有在主界面选择启动配置呢QAQ";
                    AutoRunCheckbox.IsChecked = false;
                    return;
                }
                using (FileStream fs = File.OpenWrite("AutoRun.txt"))
                using (StreamWriter sw = new StreamWriter(fs))
                    foreach (var i in list)
                        sw.WriteLine(i.ConfigGuid);
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                    foreach (var i in list)
                        sw.WriteLine("..." + i.ConfigGuid.ToString("N").Substring(20) + " " + i.ConfigName);
                AutoRunListTextBox.Text = sb.ToString();
                key.SetValue("ArkDesktop", GetType().Assembly.Location + " -autorun");
            }
            else key.DeleteValue("ArkDesktopHelper", false);
        }

        private void AutoUpdateCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (AutoUpdateCheckBox.IsChecked == true) File.Create("AutoUpdate.flag");
            else if (File.Exists("AutoUpdate.flag")) File.Delete("AutoUpdate.flag");
        }

        private string _updateUrl;

        private void QueryUpdate_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                (var a, var b) = ArkDesktop.CoreKit.UpdateChecker.GetUpdateInfo();
                Dispatcher.Invoke(() =>
                {
                    if (a == "Latest") UpdateTextBlock.Text = "已经是最新版本了哦QwQ";
                    else if (a == "") UpdateTextBlock.Text = "更新查询失败QAQ";
                    else
                    {
                        UpdateButton.IsEnabled = true;
                        UpdateTextBlock.Text = b;
                        _updateUrl = a;
                    }
                });
            });
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(_updateUrl);
        }

        private void GuidGenerateButton_Click(object sender, RoutedEventArgs e)
        {
            Guid guid = Guid.NewGuid();
            GuidTextBox.Text = guid.ToString();
        }
    }
}
