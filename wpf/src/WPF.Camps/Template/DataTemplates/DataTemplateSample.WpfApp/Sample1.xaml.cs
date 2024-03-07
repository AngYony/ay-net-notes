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
    /// Sample1.xaml 的交互逻辑
    /// </summary>
    public partial class Sample1 : Window
    {
        public Sample1()
        {
            InitializeComponent();

            var colors = new[] { "#FFA500", "#FFFFE0", "#F0FFFF", "#CD853F", "#5F9EA0", "#9370DB", "#EE82EE", "#FFB6C1", "#7FFFD4", "#D2B48C", "#FF69B4" };

            list.ItemsSource = Enumerable.Range(1, 10).Select(a => new { Index = a, MyColor = colors[a] });
             
            
        }
    }
}
