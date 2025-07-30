using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.Utils
{
    public static class EnumExtension
    {
        public static T GetEnumValue<T>(this string text) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), text, true);
        }
    }
}
