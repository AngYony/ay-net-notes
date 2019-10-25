using System;

namespace Delegates_Sample
{
    internal static class DelegatesTest
    {
        //声明一个委托
        private delegate void IntMethodInvoker(int x);
        //该委托表示的方法有两个double型参数，返回类型为double
        private delegate double TwoLongsOp(double first, double second);
        //方法不带参数的委托，返回string
        private delegate string GetString();

        static void ShowInt(int x)
        {
            Console.WriteLine("这是一个数字："+x);
        }
        static double ShowSum(double first,double second)
        {
            return first + second;
        }


        public static void Run()
        {
            int a = 10;
            //调用委托形式一
            IntMethodInvoker showIntMethod = new IntMethodInvoker(ShowInt);
            showIntMethod(a);

            //调用委托形式二
            TwoLongsOp showSumMethod = ShowSum;
            double sum= showSumMethod.Invoke(1.23, 2.33);
            Console.WriteLine("两数之和："+sum);

            GetString showString = a.ToString;
            string str=showString();
            Console.WriteLine("使用委托调用a.ToString()方法："+str);
        }


    }
}