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
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class ConfigSelect : UserControl
    {
        private readonly ConfigManager manager;

        public ConfigSelect(ConfigManager manager)
        {
            InitializeComponent();
            this.manager = manager;
            manager.ScanConfigs();
            foreach (var i in manager.Configs)
            {
                configList.Children.Add(new ConfigCard(i));
            }
        }
    }
}
