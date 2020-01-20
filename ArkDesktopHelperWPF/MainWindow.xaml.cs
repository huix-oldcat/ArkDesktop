using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
        List<ConfigInfo> validStartupConfig = new List<ConfigInfo>();

        private bool ValidAutorun()
        {
            var args = Environment.GetCommandLineArgs();
            string realPath = AppDomain.CurrentDomain.BaseDirectory;
            string autorunFile = System.IO.Path.Combine(realPath, "AutoRun.txt");
            if (args.Length != 2 || args[1] != "-autorun" || File.Exists(autorunFile) == false) return false;
            using (StringReader sr = new StringReader(File.ReadAllText(autorunFile)))
            {
                while (true)
                {
                    string s = sr.ReadLine();
                    if (s == "") break;
                    try
                    {
                        Guid g = Guid.Parse(s);
                        foreach (var i in manager.Configs)
                            if (i.ConfigGuid == g && i.LaunchModule != null)
                                validStartupConfig.Add(i);
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }
            }
            return validStartupConfig.Any();
        }

        public MainWindow()
        {
            manager = new ConfigManager(AppDomain.CurrentDomain.BaseDirectory);
            manager.ScanPlugins();
            manager.ScanConfigs();
            if (ValidAutorun() == false)
            {
                InitDrawer();
                InitializeComponent();
                drawerItemsListBox.ItemsSource = DrawerItems;
                drawerItemsListBox.SelectedIndex = 0;
                if (File.Exists("AutoUpdate.flag"))
                {
                    MainSnackbar.MessageQueue.Enqueue("正在检查更新...");
                    Task.Factory.StartNew(() =>
                    {
                        (string a, _) = UpdateChecker.GetUpdateInfo();
                        if (a != "" && a != "Latest")
                        {
                            Dispatcher.Invoke(() => MainSnackbar.MessageQueue.Enqueue("发现新版本!请前往全局设置下载"));
                        }
                        else if (a == "Latest")
                        {
                            Dispatcher.Invoke(() => MainSnackbar.MessageQueue.Enqueue("已是最新版本"));
                        }
                        else Dispatcher.Invoke(() => MainSnackbar.MessageQueue.Enqueue("查询过程发生错误"));
                    });
                }
            }
            else StartMultiConfig(validStartupConfig, false);
        }

        private void InitDrawer()
        {
            DrawerItems = new DrawerItemData[3];
            DrawerItems[0] = new DrawerItemData { Name = "主界面", Control = new ConfigSelect(manager, (i) => StartMultiConfig(i)) };
            DrawerItems[1] = new DrawerItemData { Name = "全局设置", Control = new GlobalSetting((DrawerItems[0].Control as ConfigSelect).RequestDelegate) };
            DrawerItems[2] = new DrawerItemData { Name = "关于", Control = new AboutInfo() };
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

        private void StartMultiConfig(List<ConfigInfo> configInfos, bool needClose = true)
        {
            List<ManualResetEvent> events = new List<ManualResetEvent>();
            if (needClose) Close();
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

        public delegate List<ConfigInfo> RequestConfigList();
    }
}
