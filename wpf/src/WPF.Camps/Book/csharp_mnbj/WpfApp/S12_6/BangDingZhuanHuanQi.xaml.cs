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
    /// BangDingZhuanHuanQi.xaml 的交互逻辑
    /// </summary>
    public partial class BangDingZhuanHuanQi : Window
    {
        public BangDingZhuanHuanQi()
        {
            InitializeComponent();
        }
    }

    public class FillColorBrushConverter : IValueConverter
    {

        //将从数据源中获取的数据转换为目标数据
        public object Convert(
            object value, //数据源获取的的值
            Type targetType,  //指示绑定目标所需要的类型
            object parameter, //在使用转换器时传递的一个自定义参数
            System.Globalization.CultureInfo culture)
        {
            // 取出字符串
            string strColor = value as string;
            if (!string.IsNullOrEmpty(strColor))
            {
                Color c;
                try
                {
                    // 进行转化
                    c = (Color)System.Windows.Media.ColorConverter.ConvertFromString(strColor);
                    return new SolidColorBrush(c);
                }
                catch
                {
                    // 忽略异常
                }
            }
            // 此处不应该返回null
            return DependencyProperty.UnsetValue;
        }


        //当进行双向绑定时，把绑定的目标的值转换回数据源所需要的值
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //由于本示例不是双向绑定，因此此处直接返回null
            return null;
        }
    }
}
