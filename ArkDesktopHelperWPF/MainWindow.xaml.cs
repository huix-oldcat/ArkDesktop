using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
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

        private delegate void SingleConfig(ConfigInfo info);

        private void EnumSelectedConfig(SingleConfig work)
        {
            foreach (ConfigCard card in (main.Children[0] as ConfigSelect).configList.Children)
                if (card.isChecked)
                    work(card.info);
        }

        private void StartMultiConfig()
        {
            List<ManualResetEvent> events = new List<ManualResetEvent>();
            Close();
            PluginGuiContainer pluginGuiContainer = new PluginGuiContainer(manager);
            InstanceManager instanceManager = new InstanceManager(pluginGuiContainer);
            EnumSelectedConfig((ConfigInfo configInfo) =>
            {
                if (configInfo.LaunchModule == null) return;
                ManualResetEvent e = new ManualResetEvent(false);
                events.Add(e);
                Thread thread = new Thread(new ThreadStart(() =>
                {
                    instanceManager.LaunchModule(configInfo);
                    e.Set();
                }));
                thread.IsBackground = true;
                thread.Start();
            });
            if (events.Count > 0) foreach (var i in events) i.WaitOne();
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {
                case "runButton":
                    StartMultiConfig();
                    break;
            }
        }
    }
}
