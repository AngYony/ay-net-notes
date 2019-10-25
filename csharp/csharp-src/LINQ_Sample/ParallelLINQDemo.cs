using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LINQ_Sample
{
    class ParallelLINQDemo
    {
        public static IEnumerable<int> SampleData()
        {
            const int arraySize = 50000000;
            var r = new Random();
            //连续50000000次随机取出小于140的数字
            Enumerable.Range(0, arraySize).Select(x => r.Next(140)).ToList();
            Enumerable.Range(0, arraySize).Select(x => r.Next(140)).ToList();
            Enumerable.Range(0, arraySize).Select(x => r.Next(140)).ToList();

            return Enumerable.Range(0, arraySize).Select(x => r.Next(140)).ToList();

        }


        public static void Run()
        {
            var data = SampleData();
            var res = (from x in data.AsParallel() where Math.Log(x) < 4 select x).Average();
            Console.WriteLine(res);
            var res2 = data.AsParallel().Where(x => Math.Log(x) < 4).Select(x => x).Average();
            Console.WriteLine(res2);

            var result= (from x in Partitioner.Create((List<int>)data, true)
                         .AsParallel() where Math.Log(x) < 4 select x)
                         .Average();
        }

        public static void CancellationRun()
        {
            var cts = new CancellationTokenSource();
            var data = SampleData();
               Task.Run(()=> {
                try
                {
                    var res = (from x in data.AsParallel().WithCancellation(cts.Token)
                               where Math.Log(x) < 4
                               select x).Average();
                    Console.WriteLine(res);
                }
                catch (OperationCanceledException ex)
                {
                    Console.WriteLine(ex.Message);
                }

            });

            Console.WriteLine("取消吗?");
            string input = Console.ReadLine();
            if (input.ToLower().Equals("y"))
            {
                cts.Cancel();
            }
        }
    }
}
