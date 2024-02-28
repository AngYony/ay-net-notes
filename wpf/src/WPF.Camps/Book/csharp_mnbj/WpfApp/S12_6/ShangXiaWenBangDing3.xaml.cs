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

namespace S12_6
{
    /// <summary>
    ///  将一个元素的属性与同一个元素的另一个属性进行绑定
    /// </summary>
    public partial class ShangXiaWenBangDing3 : Window
    {
        public ShangXiaWenBangDing3()
        {
            InitializeComponent();
        }
    }
    //使用MultiBinding将Button的Content属性与ActualWidth和ActualHeight两个属性进行绑定,并加载一个转换器，将从数据源获得的两个Double值转换为字符串
    public class DoubleConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // 取出两个double值，并转化为字符串
            string s = ((double)values[0]).ToString("N0") + " × " + ((double)values[1]).ToString("N0");
            return s;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}
