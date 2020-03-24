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
            bool firstUse = Directory.Exists(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "configs")) == false;
            bool autoUpdate = File.Exists(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AutoUpdate.flag"));
            manager = new ConfigManager(AppDomain.CurrentDomain.BaseDirectory);
            manager.ScanPlugins();
            manager.ScanConfigs();
            if (ValidAutorun() == false)
            {
                InitDrawer();
                InitializeComponent();
                drawerItemsListBox.ItemsSource = DrawerItems;
                drawerItemsListBox.SelectedIndex = 0;
                if (firstUse) MainSnackbar.MessageQueue.Enqueue("第一次使用本软件?请阅读软件目录下的操作说明和界面介绍");
                if (autoUpdate)
                {
                    MainSnackbar.MessageQueue.Enqueue("正在检查更新...");
                    Task.Factory.StartNew(() =>
                    {
                        (string a, _) = UpdateChecker.GetUpdateInfo(manager.Modules, App.version);
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
            else
            {
                Task<string> task = autoUpdate ? new Task<string>(() =>
                       {
                           (string a, _) = UpdateChecker.GetUpdateInfo(manager.Modules, App.version);
                           return a;
                       }) : null;
                StartMultiConfig(validStartupConfig, false, task);
            }
        }

        private void InitDrawer()
        {
            DrawerItems = new DrawerItemData[3];
            DrawerItems[0] = new DrawerItemData { Name = "主界面", Control = new ConfigSelect(manager, (i) => StartMultiConfig(i), BroadcaseMessage) };
            DrawerItems[1] = new DrawerItemData { Name = "全局设置", Control = new GlobalSetting((DrawerItems[0].Control as ConfigSelect).RequestDelegate, manager) };
            DrawerItems[2] = new DrawerItemData { Name = "关于", Control = new AboutInfo() };
        }

        private void CloseWindowButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Application.Current.Shutdown();
            Environment.Exit(0);
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
        public delegate void RequestBroadcast(string message);

        private void BroadcaseMessage(string message)
        {
            MainSnackbar.MessageQueue.Enqueue(message);
        }

        private void StartMultiConfig(List<ConfigInfo> configInfos, bool needClose = true, Task<string> task = null)
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
            if (task != null)
            {
                task.Start();
                task.Wait();
                string s = task.Result;
                if (s != null && s != "" && s != "Latest") pluginGuiContainer.BroadcastNotice("发现新版本!请前往全局设置下载");
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

        private void Main_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop) == false) return;
            e.Effects = DragDropEffects.Copy;
            DragNotice.Visibility = Visibility.Visible;
        }

        private void Main_DragLeave(object sender, DragEventArgs e)
        {
            DragNotice.Visibility = Visibility.Hidden;
        }

        private void ImportPackage(string path)
        {
            void addText(string t) => Dispatcher.Invoke(() => DropDetails.Text += t);
            addText(path + "\n");
            using (var fs = File.OpenRead(path))
            {
                (var a, var b) = PackageManager.ReadPackageInfo(fs);
                if (a.version == 255)
                {
                    addText("    无效的包\n");
                    return;
                }
                if (a.version == 254)
                {
                    addText("    版本不支持\n");
                    return;
                }
                foreach (var i in b)
                {
                    var found = from v in manager.Configs where v.ConfigGuid == i.configGuid select true;
                    var st = "";
                    var exportPath = "";
                    addText(string.Format("    读入配置{0}({1}) ", i.defaultConfigName, i.configGuid.ToString().Substring(24)));
                    if (found.Any())
                    {
                        st = "重复的配置";
                    }
                    else if (Directory.Exists(System.IO.Path.Combine(manager.rootPath, "configs", i.defaultConfigName)))
                    {
                        string newName = i.defaultConfigName + "_" + Guid.NewGuid().ToString().Substring(28);
                        exportPath = System.IO.Path.Combine(manager.rootPath, "configs", newName);
                        st = "GUID未重复但配置名重复,已重命名配置为" + newName;
                    }
                    else
                    {
                        exportPath = System.IO.Path.Combine(manager.rootPath, "configs", i.defaultConfigName);
                        st = "导入成功";
                    }
                    if (exportPath != "")
                    {
                        Directory.CreateDirectory(exportPath);
                        fs.Position = i.firstFilePosition;
                        PackageManager.ExtractFiles(exportPath, fs);
                    }
                    addText(string.Format("==> {0}\n", st));
                }
            }
        }

        private void Main_Drop(object sender, DragEventArgs e)
        {
            var datas = e.Data.GetData(DataFormats.FileDrop) as string[];
            DropDetails.Text = "";
            Task.Factory.StartNew(() =>
            {
                foreach (var data in datas)
                    if (data.EndsWith(".akdpkg"))
                        ImportPackage(data);
                Dispatcher.Invoke(() => (DrawerItems[0].Control as ConfigSelect).LoadConfigs());
                Dispatcher.Invoke(() => DropDetails.Text += "读入完成,5秒后关闭提示框");
                Task.Factory.StartNew(() =>
                {
                    Thread.Sleep(5000);
                    DragNotice.Dispatcher.Invoke(() => DragNotice.Visibility = Visibility.Hidden);
                });
            });
        }
    }
}
