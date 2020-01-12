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
    /// ConfigCard.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigCard : UserControl
    {
        private readonly ConfigInfo info;
        private readonly MainWindow.StartConfigInvoker startConfig;

        public ConfigCard(ConfigInfo info, MainWindow.StartConfigInvoker startConfig)
        {
            InitializeComponent();
            this.info = info;
            this.startConfig = startConfig;
            name.Text = info.ConfigName;
            description.Text = info.Description == "" ? "暂无描述" : info.Description;
            if (info.LaunchModule == null)
                invalidMask.Visibility = Visibility.Visible;
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            runMask.Visibility = Visibility.Visible;
        }

        private void RunMask_MouseLeave(object sender, MouseEventArgs e)
        {
            runMask.Visibility = Visibility.Hidden;
        }

        private void RunMask_MouseDown(object sender, MouseButtonEventArgs e)
        {
            rect.Fill = new SolidColorBrush(Color.FromArgb(175, 100, 100, 100));
        }

        private void RunMask_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rect.Fill = new SolidColorBrush(Color.FromArgb(175, 255, 255, 255));
            startConfig(info);
        }
    }
}
