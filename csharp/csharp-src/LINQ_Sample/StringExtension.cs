using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Sample
{
    public static class StringExtension
    {
        public static string FirstName (this string name)
        {
            int ix = name.LastIndexOf(' ');
            return name.Substring(0, ix);
        }

        public static string LastName (this string name)
        {
            int ix = name.LastIndexOf(' ');
            return name.Substring(ix + 1);
        }

        public static void WriteLine(this string str)
        {
            Console.WriteLine(str);
        }
    }
}
