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
    /// 通过绑定来设置矩形的填充颜色
    /// </summary>
    public partial class ShangXianWenBangDing2 : Window
    {
        public ShangXianWenBangDing2()
        {
            InitializeComponent();
        }
    }


    /// <summary>
    /// 转换器
    /// </summary>
    public class ColorConverter : IMultiValueConverter
    {
        //从数据源中获取的数据转换为绑定目标所需要的数据
        public object Convert(object[] values,
            Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // 取出四个double值
            byte a = System.Convert.ToByte(values[0]);
            byte r = System.Convert.ToByte(values[1]);
            byte g = System.Convert.ToByte(values[2]);
            byte b = System.Convert.ToByte(values[3]);
            // 返回Color结构实例
            return Color.FromArgb(a, r, g, b);
        }

        //将目标数据转化为数据源中需要的数据，只在双向绑定时才生效，所以此处直接返回null不做处理。
        public object[] ConvertBack(object value,
            Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            return null;
        }
    }
}