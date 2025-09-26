using AY.LearningTag.App.ControllSample.TabControl;
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
using System.Windows.Shapes;

namespace AY.LearningTag.App.ControllSample
{
    /// <summary>
    /// ControllMain.xaml 的交互逻辑
    /// </summary>
    public partial class ControllMain : Window
    {
        public ControllMain()
        {
            InitializeComponent();
        }

        private void btnListBox_Click(object sender, RoutedEventArgs e)
        {
            new ListBoxSample().ShowDialog();
        }

        private void btnButton_Click(object sender, RoutedEventArgs e)
        {
            new ButtonSample().ShowDialog();
        }

        private void TabControl_Click(object sender, RoutedEventArgs e)
        {
            new TabControlSample().ShowDialog();
        }
    }
}
