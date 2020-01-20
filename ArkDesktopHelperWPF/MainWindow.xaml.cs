using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public class DrawerItemData
        {
            public string Name { get; set; }
            public Control Control { get; set; }
        }
        public DrawerItemData[] DrawerItems { get; set; }
        ConfigManager manager;

        public MainWindow()
        {
            manager = new ConfigManager(AppDomain.CurrentDomain.BaseDirectory);
            manager.ScanPlugins();
            InitDrawer();
            InitializeComponent();
            drawerItemsListBox.ItemsSource = DrawerItems;
            drawerItemsListBox.SelectedIndex = 0;
        }

        private void InitDrawer()
        {
            DrawerItems = new DrawerItemData[2];
            DrawerItems[0] = new DrawerItemData { Name = "主界面", Control = new ConfigSelect(manager, StartMultiConfig) };
            DrawerItems[1] = new DrawerItemData { Name = "关于", Control = new AboutInfo() };
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

        public delegate void RequestStart(List<ConfigInfo> configInfos);

        private void StartMultiConfig(List<ConfigInfo> configInfos)
        {
            List<ManualResetEvent> events = new List<ManualResetEvent>();
            Close();
            PluginGuiContainer pluginGuiContainer = new PluginGuiContainer(manager);
            InstanceManager instanceManager = new InstanceManager(pluginGuiContainer);
            foreach (var configInfo in configInfos)
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
            }
            if (events.Count > 0) foreach (var i in events) i.WaitOne();
            Application.Current.Shutdown();
        }

        private void drawerItemsListBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //until we had a StaysOpen glag to Drawer, this will help with scroll bars
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is System.Windows.Controls.Primitives.ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }
            MenuToggleButton.IsChecked = false;
        }
    }
}
