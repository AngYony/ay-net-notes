using System;

namespace Delegates_Sample
{
    internal class MathOperations_V2
    {
        public static void MultiplyByTwo(double value)
        {
            double result = value * 2;
            Console.WriteLine($"{value}*2={result}");
        }

        public static void Square(double value)
        {
            double result = value * value;
            Console.WriteLine($"{value}*{value}={result}");
        }
    }

    internal class MathOperations_V2_Program
    {
        private delegate void DoubleOp2(double x);

        private static void ProcessAndDisplayNumber(Action<double> action, double value)
        {
            Console.WriteLine("调用ProcessAndDisplayNumber方法：value=" + value);
            action(value);
        }

        public static void Run()
        {
            Action<double> operations = MathOperations_V2.MultiplyByTwo;
            operations += MathOperations_V2.Square;

            ProcessAndDisplayNumber(operations, 3);
            ProcessAndDisplayNumber(operations, 4);
            ProcessAndDisplayNumber(operations, 5);

            //在调用委托的时候，如果委托定义了方法并带有参数，一定要将参数传入
        }

        private static void ProcessAndDisplayNumber(DoubleOp2 action, double value)
        {
            action(value);
        }

        public static void Run_2()
        {
            DoubleOp2[] operations = {
                MathOperations_V2.MultiplyByTwo,
                MathOperations_V2.Square
            };

            for (int i = 0; i < operations.Length; i++)
            {
                Console.WriteLine($"Using operations[{i}]:");
                ProcessAndDisplayNumber(operations[i], 2);
                ProcessAndDisplayNumber(operations[i], 3);
                ProcessAndDisplayNumber(operations[i], 4);
            }

            Func<double, double>[] operations2 ={
                MathOperations.MultiplyByTwo,
                MathOperations.Square
            };
        }
    }

    internal class MathOperations_V2_Program_V2
    {
        private static void One()
        {
            Console.WriteLine("调用One()方法");
            throw new Exception("Error in one");
        }

        private static void Two()
        {
            Console.WriteLine("调用Two()方法");
        }

        public static void Run()
        {
            Action d1 = One;
            d1 += Two;
            try
            {
                d1();
            }
            catch (Exception)
            {
                Console.WriteLine("调用d1出错了");
            }
        }

        public static void Run2()
        {
            Action d1 = One;
            d1 += Two;
            Delegate[] delegates = d1.GetInvocationList();
            foreach (Action d in delegates)
            {
                try
                {
                    d();
                }
                catch (Exception)
                {
                    Console.WriteLine("调用出错了！！");
                }
            }
        }

        public static void Run3()
        {
            //推荐使用lambda表达式
            string start = "厉害了，";
            Func<string, string> print = delegate (string param)
            {
                return start + param;
            };
            Console.WriteLine(print("我的国！"));
        }

        public static void Run4()
        {
            //使用Lambda表达式进行匿名方法的定义
            string start = "厉害了，";
            Func<string, string> lambda = param => start + param;
            Console.WriteLine(lambda("我的C#!!!"));
        }
    }
}