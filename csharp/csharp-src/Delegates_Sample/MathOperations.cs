using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates_Sample
{

    class MathOperations
    {
        public static double MultiplyByTwo(double value)
        {
            double result = value * 2;
            Console.WriteLine($"{value}*2={result}");
            return result;
        }

        public static double Square(double value)
        {
            double result = value * value;
            Console.WriteLine($"{value}*{value}={result}");
            return result;
        }

        

    }



    class MathOperations_Program
    {
        delegate double DoubleOp(double x);
        public static void Run()
        {
            DoubleOp[] operations = {
                MathOperations.MultiplyByTwo,
                MathOperations.Square
            };


            for (int i = 0; i < operations.Length; i++)
            {
                Console.WriteLine($"Using operations[{i}]:");
                ProcessAndDisplayNumber(operations[i], 2);
                ProcessAndDisplayNumber(operations[i], 3);
                ProcessAndDisplayNumber(operations[i], 4);
            }
        }

        static void ShowDouble(DoubleOp op, double double_num)
        {
            double result = op(double_num);
            Console.WriteLine("值为：" + result);
        }

        public static void Run_2()
        {

            Func<double, double>[] operations2 ={
                MathOperations.MultiplyByTwo,
                MathOperations.Square
            };

            Predicate<int> pre = b => b > 5;



            Func<double, double> operations = MathOperations.MultiplyByTwo;
            operations += MathOperations.Square;

            ProcessAndDisplayNumber(operations, 3);

            //Func<double,double> operation1 = MathOperations.MultiplyByTwo;
            //Func<double, double> operation2 = MathOperations.Square;

            //Func<double,double> operations2= operation1 + operation2;
        }

        static void ProcessAndDisplayNumber(DoubleOp action, double value)
        {
            double result = action(value);
            Console.WriteLine($"Value is {value },result of operation is {result}");
        }

        static void ProcessAndDisplayNumber(Func<double, double> action, double value)
        {
            double result = action(value);
            Console.WriteLine($"Value is {value },result of operation is {result}");
        }
    }

}
