using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace S2
{
    class Program
    {
        //Program.cs中的调用代码
        static async Task Main(string[] args)
        {
            Console.WriteLine("start");
            //await ProgressTest.CallMyMethodAsync();
            await TaskCompletedTest.ProcessTasksErrorAsync();
            Console.WriteLine("end");
            Console.ReadLine();
        }

    }
}
