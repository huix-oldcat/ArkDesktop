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
        public readonly ConfigInfo info;
        public bool isChecked = false;

        public ConfigCard(ConfigInfo info)
        {
            InitializeComponent();
            this.info = info;
            name.Text = info.ConfigName;
            description.Text = info.Description == "" ? "暂无描述" : info.Description;
            if (info.LaunchModule == null) errorTag.ToolTip = "一个或多个插件未加载";
            else errorTag.Visibility = Visibility.Hidden;
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            runMask.Visibility = Visibility.Visible;
        }

        private void RunMask_MouseLeave(object sender, MouseEventArgs e)
        {
            if (isChecked == false) runMask.Visibility = Visibility.Hidden;
        }

        private void RunMask_MouseDown(object sender, MouseButtonEventArgs e)
        {
            rect.Fill = new SolidColorBrush(Color.FromArgb(175, 100, 100, 100));
        }

        private void RunMask_MouseUp(object sender, MouseButtonEventArgs e)
        {
            rect.Fill = new SolidColorBrush(Color.FromArgb(175, 255, 255, 255));
            isChecked = !isChecked;
            checkedIcon.Kind = isChecked ? MaterialDesignThemes.Wpf.PackIconKind.RadioboxMarked : MaterialDesignThemes.Wpf.PackIconKind.RadioboxBlank;
        }
    }
}
