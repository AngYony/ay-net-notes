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

namespace DataTemplateSample.WpfApp
{
    /// <summary>
    /// Sample4.xaml 的交互逻辑
    /// </summary>
    public partial class Sample4 : Window
    {
        public Sample4()
        {
            InitializeComponent();
            myCom.ItemsSource = new List<object>()
            {
                new {UserName="张三" ,Age=100},
                new {UserName="张三" ,Age=100},
            };
        }
    }
}
