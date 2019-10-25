using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async_Sample
{
    class ErrorHanding
    {
        public async static Task ThrowAfter(int ms, string message)
        {
            await Task.Delay(ms);
            throw new Exception(message);
        }

        
        public async static void DontHandle()
        {
            try
            {
                await ThrowAfter(200, "first");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        public static async void StartTwoTask()
        {
            try
            {
                await ThrowAfter(2000, "first");
                await ThrowAfter(1000, "second");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async static void StartTwoTaskParallel()
        {
            try
            {
                Task t1 = ThrowAfter(2000, "first");
                Task t2 = ThrowAfter(1000, "second");
                await Task.WhenAll(t1, t2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static async void ShowAggregatedException()
        {
            
           Task taskResult = null;
            try
            {
                Task t1 = ThrowAfter(2000, "first");
                Task t2 = ThrowAfter(1000, "second");
                await (taskResult = Task.WhenAll(t1, t2));

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                foreach(var ex1 in taskResult.Exception.InnerExceptions)
                {
                    Console.WriteLine(ex1.Message);
                }
            }
        }
    }
}
