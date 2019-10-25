using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates_Sample
{
    
    class LambdaTest
    {
        public static void Run()
        {
            Func<string, string> oneParam = s => $"将{s}转换为大写：" + s.ToUpper();
            //调用
            Console.WriteLine(oneParam("abc"));

            Func<double, double, double> twoParam = (x, y) => x * y;
            Console.WriteLine("2*3=" + twoParam(2, 3));

            //在小括号中指定参数类型
            Func<double, double, double> twoParamsWithTypes = (double x, double y) => x + y;
            Console.WriteLine("2.3+1.3=" + twoParamsWithTypes(2.3, 1.3));

            //多行语句
            Func<string, string, string> joinString = (str1, str2) =>
              {
                  str1 += str2;
                  return str1.ToUpper();

              };
            Console.WriteLine(joinString("abc", "def"));


            //闭包
            int someVal = 5;
            Func<int, int> f = x => x + someVal;

            Action a = () => Console.WriteLine("无参数");
            a();


        }
    }
}
