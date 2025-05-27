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

namespace ShapeSample.WpfApp
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new LineSample().ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new RectangleSample().ShowDialog();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            new VisualBrushSample().ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            new EllipseSample().ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            new PathSample().ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            new PathSample2().ShowDialog();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            new PathSample3().ShowDialog();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            new PathSample4().ShowDialog();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            new PathSample5().ShowDialog();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            new RenderTransformSample().ShowDialog();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            new LayoutTransformSample().ShowDialog();
        }
    }
}
