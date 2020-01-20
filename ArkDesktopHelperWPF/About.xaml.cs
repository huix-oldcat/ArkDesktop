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
    /// About.xaml 的交互逻辑
    /// </summary>
    public partial class AboutInfo : UserControl
    {
        public AboutInfo()
        {
            InitializeComponent();
        }

        private void GoWildButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender.Equals(GitHubButton)) System.Diagnostics.Process.Start("https://www.github.com/huix-oldcat/ArkDesktop");
            else if(sender.Equals(WebsiteButton)) System.Diagnostics.Process.Start("https://akd.huix.cc/");
            else if(sender.Equals(QQGroupButton)) System.Diagnostics.Process.Start("https://jq.qq.com/?_wv=1027&k=5j3ooR8");
        }
    }
}
