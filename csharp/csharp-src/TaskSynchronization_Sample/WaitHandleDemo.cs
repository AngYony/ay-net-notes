using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSynchronization_Sample
{
    class WaitHandleDemo
    {

        static int TakesAWhile(int x,int ms)
        {
            Task.Delay(ms).Wait();
            return 42;
        }

        delegate int TakesAWhileDelegate(int x, int ms);
        public static void Run()
        {
            
            TakesAWhileDelegate d1 = TakesAWhile;
            IAsyncResult ar= d1.BeginInvoke(1, 3000, null, null);
            while (true)
            {
                if (ar.AsyncWaitHandle.WaitOne(50))
                {
                    Console.WriteLine("Can get the result now");
                    break;
                }
            }
            int result = d1.EndInvoke(ar);
            Console.WriteLine("result:"+result);
        }
    }
}
