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

namespace S12_9
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnxKey_Click(object sender, RoutedEventArgs e)
        {
            new StyleXKey().ShowDialog();
        }

        private void btnNoxKey_Click(object sender, RoutedEventArgs e)
        {
            new StyleNoXKey().ShowDialog();
        }

        private void btnStyleTrigger_Click(object sender, RoutedEventArgs e)
        {
            new StyleTrigger().ShowDialog();
        }
    }
}
