using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeiTuo.Sample
{
    internal class Sample01
    {


        public void Run()
        {
            // 方法内定义一个简单的匿名方法
            string Hi() => "这是什么";
            // 调用方法
            Console.WriteLine(Hi());

            //方法内定义一个复杂的匿名方法
            Func<string, string> fun = (string p) =>
            {
                return "传入的参数是：" + p;
            };

            Console.WriteLine(fun("参数A"));
        }



    }
}
