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

namespace StyleResourceSample.WpfApp
{
    /// <summary>
    /// Sample2.xaml 的交互逻辑
    /// </summary>
    public partial class Sample2 : Window
    {
        public Sample2()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Resources["SolidColor"]=new SolidColorBrush(Colors.Blue);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // 更改第一个资源项
            SolidColorBrush brushA = new SolidColorBrush(Colors.Red);
            this.Resources["brush1"] = brushA;
            // 更改第二个资源项
            SolidColorBrush brushB = new SolidColorBrush(Colors.DeepPink);
            this.Resources["brush2"] = brushB;
        }
    }
}
