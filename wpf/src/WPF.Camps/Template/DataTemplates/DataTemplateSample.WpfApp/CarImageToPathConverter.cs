using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DataTemplateSample.WpfApp
{
    /// <summary>
    /// 图片名称转换为路径
    /// </summary>
    public class CarImageToPathConverter : IValueConverter
    {
        //数据绑定源流向目标被调用
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string uriStr = $"Images/{(string)value}.jpg";
            return new BitmapImage(new Uri(uriStr, UriKind.Relative));
        }

        //未被用到
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
