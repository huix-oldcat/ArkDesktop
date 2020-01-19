using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
using ArkDesktop.CoreKit;

namespace ArkDesktopHelperWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ConfigManager manager;

        public void StartConfig(ConfigInfo info)
        {
            Close();
            PluginGuiContainer pluginGuiContainer = new PluginGuiContainer(manager);
            InstanceManager instanceManager = new InstanceManager(new InstanceHelper(pluginGuiContainer));
            instanceManager.Start();
            instanceManager.LaunchModule(info);
            Application.Current.Shutdown();
        }

        public MainWindow()
        {
            manager = new ConfigManager(AppDomain.CurrentDomain.BaseDirectory);
            manager.ScanPlugins();
            InitializeComponent();
            ConfigSelect control1 = new ConfigSelect(manager);
            main.Children.Add(control1);
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Application.Current.Shutdown();
        }

        private void MinWindowButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ColorZone_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
