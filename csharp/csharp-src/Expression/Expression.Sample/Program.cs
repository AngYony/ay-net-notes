using System.Linq.Expressions;

namespace ExpressionSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ParameterExpression a = Expression.Parameter(typeof(int), "i");
            ParameterExpression b = Expression.Parameter(typeof(int), "j");
            //生成结点r1
            Expression r1 = Expression.Multiply(a, b);      //乘法运行

            ParameterExpression c = Expression.Parameter(typeof(int), "x");
            ParameterExpression d = Expression.Parameter(typeof(int), "y");
            //生成结点r2
            Expression r2 = Expression.Multiply(c, d);      //乘法运行

            //将结点r1和r2组合起来，生成终结点
            Expression result = Expression.Add(r1, r2);     //相加
            
            //生成表达式
            Expression<Func<int, int, int, int, int>> func = Expression.Lambda<Func<int, int, int, int, int>>(result, a, b, c, d);
            //执行表达式树
            var com = func.Compile();
            Console.WriteLine("表达式" + func);
            Console.WriteLine(com(12, 12, 13, 13));
            Console.ReadKey();
        }
    }
}
