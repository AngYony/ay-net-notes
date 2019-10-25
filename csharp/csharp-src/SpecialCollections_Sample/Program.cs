using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialCollections_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 不可变集合示例
            //Account_Program.Run();
            #endregion

            #region 使用构建器和不变的集合
            //Account_Program.Run2();
            #endregion


            #region 并发集合

            //PipelineSample.StartPipelineAsync();
            #endregion

            #region 位集合
            //BitArrayDemo.Run();
            #endregion


            #region
            //BitVector32调用
            //BitVector32Demo.Run();
            #endregion

            #region 可观察的集合
            ObservableDemo.Run();
            #endregion

            Console.Read();
        }
    }
}
