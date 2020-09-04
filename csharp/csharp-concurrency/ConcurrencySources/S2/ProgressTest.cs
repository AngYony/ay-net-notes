using System;
using System.Threading.Tasks;

namespace S2
{
    public class ProgressTest
    {
        private static int count = 0;

        //方法的声明中使用了async关键字，返回的是Task指定的类型，这里Task没有指定类型，因此不需要返回语句
        //如果去掉async，表示返回的是一个Task对象，程序会报错
        private static async Task MyMethodAsync(IProgress<double> progress = null)
        {
            double percentComplete = 0;
            while (++count < 10)
            {
                await Task.Delay(1000); //为了看到打印效果，每隔一秒等待一下
                if (progress != null)
                {
                    percentComplete = 10 * count;
                    progress.Report(percentComplete);
                }
            }
        }

        //调用上述方法
        public static async Task CallMyMethodAsync()
        {
            var progress = new Progress<double>();
            progress.ProgressChanged += (sender, args) =>
            {
                //输出上文传入的percentComplete
                Console.WriteLine(args);
            };
            await MyMethodAsync(progress);
        }
    }
}