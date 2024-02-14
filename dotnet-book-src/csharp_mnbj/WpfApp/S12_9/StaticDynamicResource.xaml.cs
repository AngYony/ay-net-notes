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

namespace S12_9
{
    /// <summary>
    /// 静态资源与动态资源的引用
    /// </summary>
    public partial class StaticDynamicResource : Window
    {
        public StaticDynamicResource()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
