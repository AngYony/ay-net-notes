using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.Shared
{
    public class MyHelper
    {
        public static string GetColor()
        {
            var s = "#";
            var chars = new[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E' };

            for (int i = 1; i <= 6; i++)
            {
                var ran = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
                var index = ran.Next(0, chars.Length - 1);
                s += chars[index];
            }
            return s;
        }
    }
}
