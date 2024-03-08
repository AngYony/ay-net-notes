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
    /// Sample3.xaml 的交互逻辑
    /// </summary>
    public partial class Sample3 : Window
    {
        public Sample3()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //获取app.xaml引用的资源
            var solidColor = App.Current.FindResource("SolidColor");
            var style = App.Current.FindResource("DefaultButtonStyle");

            this.Resources["SolidColor"] = new SolidColorBrush(Colors.Fuchsia);
        }
    }
}
