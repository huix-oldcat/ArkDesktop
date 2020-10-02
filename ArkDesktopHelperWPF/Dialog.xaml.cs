using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace ArkDesktopHelperWPF
{
    /// <summary>
    /// Dialog.xaml 的交互逻辑
    /// </summary>
    public partial class Dialog :UserControl
    {
        private static string configPath = "";
        public void settingConfigPath(string s)
        {
            configPath = s;
        }
        public Dialog()
        {
            InitializeComponent();
        }

        private static void ExplorerFile(string path)
        {
            using (Process proc = new Process())
            {
                proc.StartInfo.FileName = "explorer";
                proc.StartInfo.Arguments = "/select," + path;
                proc.Start();   
            };
        }

        //否
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //TODO ,always todo
        }

        //是
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (configPath != "")
            {
                ExplorerFile(configPath);
            }
        }
    }
}
