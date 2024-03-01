using System;
using System.Linq;
using System.Threading.Tasks;

namespace S2
{
    //任务完成时的处理
    public class TaskCompletedTest
    {
        private static async Task<int> DelayAndReturnAsync(int val)
        {
            await Task.Delay(TimeSpan.FromSeconds(val));
            return val;
        }

        //当前，此方法输出2,3,1
        //我们希望它输出1,2,3
        public static async Task ProcessTasksErrorAsync()
        {
            //创建任务队列
            Task<int> taskA = DelayAndReturnAsync(2);
            Task<int> taskB = DelayAndReturnAsync(3);
            Task<int> taskC = DelayAndReturnAsync(1);
            var tasks = new[] { taskA, taskB, taskC };
            foreach (var task in tasks)
            {
                //在foreach中通过await调用，实际上还是以同步的方式一个接一个的执行的，并不是并发执行
                var result = await task;
                Console.WriteLine(result);
                //Trace.WriteLine(result);
            }
        }

        //正确的做法
        public static async Task ProcessTasksRightAsync()
        {
            //创建任务队列
            Task<int> taskA = DelayAndReturnAsync(2);
            Task<int> taskB = DelayAndReturnAsync(3);
            Task<int> taskC = DelayAndReturnAsync(1);
            var tasks = new[] { taskA, taskB, taskC };

            var processingTasks = tasks.Select(async t =>
              {
                  var result = await t;
                  Console.WriteLine(result);
              }).ToArray();

            await Task.WhenAll(processingTasks).ConfigureAwait(false);
        }
    }
}