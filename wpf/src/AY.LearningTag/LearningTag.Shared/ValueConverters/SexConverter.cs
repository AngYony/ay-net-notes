using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace LearningTag.Shared.ValueConverters
{
    public class SexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if (value is int sex)
            {
                 return sex == 1 ? "男" : (sex == 2 ? "女" : "无");
            }
            return "无";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string sex)
            {
                return sex == "男" ? 1 : (sex == "女" ? 2 : 0);
            }
            return 0;
        }
    }
}
