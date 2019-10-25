using System.Reflection;

namespace ARMD_Sample
{
    public class ClientApp
    {
        private const string CalculatorLibPath = "D:/Calculator.dll";
        private const string CalculatorLibName = "Calculator";
        
        public static void Run()
        {
            ReflectionOld();
            ReflectionNew();
        }

 
        private static object GetCalculator()
        {
            Assembly assembly = Assembly.LoadFile("D:/Calculator.dll");
            //创建实例
            return assembly.CreateInstance("Calculator.Calculator"); //命名空间和类名
        }
 

        private static void ReflectionOld()
        {
            double x = 3;
            double y = 4;
            object calc = GetCalculator();
            //方式一，不能用于.net core中
            object result = calc.GetType().InvokeMember("Add", 
                BindingFlags.InvokeMethod, null, calc, new object[] { x, y });
            System.Console.WriteLine(result);
            //方式二
            object result2 = calc.GetType().GetMethod("Add")
                .Invoke(calc, new object[] { x, y });
            System.Console.WriteLine(result2);
        }

        private static void ReflectionNew()
        {
            double x = 3;
            double y = 4;
            //方式三
            dynamic calc = GetCalculator();
            double result= calc.Add(x, y);
            System.Console.WriteLine(result);
        }

    }



}
