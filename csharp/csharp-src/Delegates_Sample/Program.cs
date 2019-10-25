using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates_Sample
{
    class Program
    {
       
        static void Main(string[] args)
        {
           // DelegatesTest.Run();

            #region 验证委托可以指定不同实例上引用的不同方法，不用考虑方法是否静态，只要方法的签名匹配委托定义即可
            //Currency_Program.Run();
            #endregion

            //MathOperations_Program.Run();

            #region 委托调用
            // BubbleSorter_Program.Run();
            #endregion

            #region 多播委托
            //MathOperations_Program.Run_2();
            #endregion

            #region 多播委托示例2
            // MathOperations_V2_Program.Run();
            #endregion

            #region 多播委托调用，模拟异常
            // MathOperations_V2_Program_V2.Run();
            #endregion

            #region 多播委托单个异常不影响事件调用
           // MathOperations_V2_Program_V2.Run2();
            #endregion

            #region 匿名委托方法调用
            //MathOperations_V2_Program_V2.Run3();
            #endregion

            #region lambda匿名委托方法调用
            // MathOperations_V2_Program_V2.Run4();
            #endregion

            #region lambda方法调用
             LambdaTest.Run();
            #endregion

            #region 事件
            //CarDealer carDealer = new CarDealer();
            //carDealer.NewCar("women");
            #endregion

            #region 事件发布和订阅
            //Consumer_Program.Run();
            #endregion

            Console.Read();
        }
    }
}
