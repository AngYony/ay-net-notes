using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQ_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            #region where一般示例 1
            //LinqDemo.Run();
            //LinqDemo.Run2();
            #endregion

            #region 筛选
            //LinqDemo.WhereRun();
            #endregion

            #region 类型筛选
            //LinqDemo.OfTypeRun();
            #endregion

            #region 复合的from子句
            //LinqDemo.FromRun();
            #endregion

            #region 排序
            //LinqDemo.OrderByRun();
            #endregion

            #region 分组
            //LinqDemo.GroupRun();
            #endregion

            #region LINQ查询中的变量
            //LinqDemo.LetRun();
            #endregion

            #region 对嵌套的对象分组
            //LinqDemo.NestingGroupRun();
            #endregion

            #region 内连接
            //LinqDemo.InnerJoinRun();
            #endregion

            #region 左外联接
            //LinqDemo.LeftOutJoinRun();
            #endregion

            #region 组联接
            //LinqDemo.ZuJoinRun();
            #endregion

            #region 集合操作
            //LinqDemo.CollectionsRun();
            #endregion

            #region 合并
            //LinqDemo.ZipRun();
            #endregion

            #region 分区
            //LinqDemo.TakeAndSkipRun();
            #endregion

            #region 聚合
            //LinqDemo.JuheRun();
            #endregion

            #region 转换操作符
            //LinqDemo.ToListRun();
            #endregion

            #region 转换操作符:ToLookup
            //LinqDemo.ToLookupRun();
            #endregion

            #region 转换操作符:Cast
            //LinqDemo.CastRun();
            #endregion


            #region 生成操作符
            //LinqDemo.BuildRun();
            #endregion

            //方式一
            // "测试".WriteLine();
            //方式二
            //StringExtension.WriteLine("测试二");

            #region 并行LINQ
            //ParallelLINQDemo.Run();
            //取消
            //ParallelLINQDemo.CancellationRun();
            #endregion

            new LeftJoinSample().Test();

            Console.Read ();
        }
    }
}
