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

namespace S12_7
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

        private void btnMyPanel_Click(object sender, RoutedEventArgs e)
        {
            CusPanel myPanel = new CusPanel();
            myPanel.ShowDialog();
        }

        private void btnGrid_Click(object sender, RoutedEventArgs e)
        {
            new GridPanel().ShowDialog();
        }

        private void btnStackPanel_Click(object sender, RoutedEventArgs e)
        {
            new MyStackPanel().ShowDialog();
        }

        private void btnDockPanel_Click(object sender, RoutedEventArgs e)
        {
            new MyDockPanel().ShowDialog();
        }

        private void btnCanvas_Click(object sender, RoutedEventArgs e)
        {
            new CanvasPanel().ShowDialog();
        }

        private void btnWrapPanel_Click(object sender, RoutedEventArgs e)
        {
            new MyWrapPanel().ShowDialog();
        }

        private void btnZIndex_Click(object sender, RoutedEventArgs e)
        {
            new ZIndexSample().ShowDialog();
        }
    }
}
