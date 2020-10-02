using ArkDesktop.CoreKit;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
        //MaterialDesign形式的 messagebox
        public async void MsgBox(String msg,object sender, RoutedEventArgs e)
        {
            Dialog dialog = new Dialog
            {
                Message = { Text = msg }
            };
            await DialogHost.Show(dialog, "RootDialog");
        }
        public async void MsgBox(String msg,String c, object sender, RoutedEventArgs e)
        {
            Dialog dialog = new Dialog
            {
                Message = { Text = msg }
            };
            dialog.settingConfigPath(c);
            await DialogHost.Show(dialog, "RootDialog");
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
                    PackageManager.PackConfigs(list, path);
                    App.Current.Dispatcher.Invoke((Action)(() =>
                    {
                        MsgBox("已导出至\n" + path + "\n是否使用文件管理器打开？", path, sender, e);
                    }));
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
