using System;
using System.Threading.Tasks;

namespace S2
{
    public class TaskExceptionTest
    {
        private static async Task ThrowExceptionAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(1));
            throw new InvalidOperationException("test");
        }

        //方式一
        private static async Task Test1Async()
        {
            try
            {
                //await必须在try中
                await ThrowExceptionAsync();
            }
            catch (InvalidOperationException)
            {
            }
        }

        //方式二
        private static async Task Test2Async()
        {
            //抛出异常，并将其存储在Task中
            Task task = ThrowExceptionAsync();

            try
            {
                //Task对象被await调用，异常在这里再次被引发。
                await task;
            }
            catch (InvalidOperationException)
            {
                //这里，异常被正确的捕获
            }
        }
    }
}