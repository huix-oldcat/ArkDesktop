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

        public GlobalSetting(MainWindow.RequestConfigList requestConfigList)
        {
            InitializeComponent();
            this.requestConfigList = requestConfigList;
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
    }
}
