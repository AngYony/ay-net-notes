using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Common.Md5Module
{
    public static class Md5Helper
    {
        public static string ToMd5(this string str)
        {
            var md5 = new MD5CryptoServiceProvider();
            //加盐
            var bytes = md5.ComputeHash(Encoding.Default.GetBytes(str + "@wy"));
            var md5Str = BitConverter.ToString(bytes).Replace("-", "");
            return md5Str;
        }
    }
}
