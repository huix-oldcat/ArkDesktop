using ArkDesktop.CoreKit;
using System;
using System.Collections.Generic;
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
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigSelect : UserControl
    {
        private readonly ConfigManager manager;
        private readonly MainWindow.RequestStart requestStart;
        private readonly MainWindow.RequestBroadcast requestBrocast;

        public ConfigSelect(ConfigManager manager, MainWindow.RequestStart requestStart, MainWindow.RequestBroadcast requestBrocast)
        {
            InitializeComponent();
            this.manager = manager;
            this.requestStart = requestStart;
            this.requestBrocast = requestBrocast;
            LoadConfigs();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {
                case "runButton":
                    requestStart?.Invoke(GetSelectedConfigs());
                    break;
                case "ExportButton":
                    string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Guid.NewGuid().ToString() + ".akdpkg");
                    requestBrocast("开始导出...");
                    var list = GetSelectedConfigs();
                    Task.Factory.StartNew(() =>
                    {
                        PackageManager.PackConfigs(list, path);
                        Dispatcher.Invoke(() => requestBrocast("已导出至" + path));
                    });
                    break;

            }
        }

        private List<ConfigInfo> GetSelectedConfigs()
        {
            List<ConfigInfo> configInfos = new List<ConfigInfo>();
            foreach (ConfigCard i in configList.Children)
                if (i.isChecked)
                    configInfos.Add(i.info);
            return configInfos;
        }

        public MainWindow.RequestConfigList RequestDelegate => GetSelectedConfigs;

        public void LoadConfigs()
        {
            configList.Children.Clear();
            manager.ScanConfigs();
            foreach (var i in manager.Configs) configList.Children.Add(new ConfigCard(i)); 
        }
    }
}
