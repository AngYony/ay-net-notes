using Dumpify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningTag.Utils
{
    public static class ThreadExtensions
    {
        public static void Timeout(Action action, TimeSpan timeoutDelay)
        {
            var thread = new Thread(() =>
            {
                try
                {
                    action.Invoke();
                }
                catch (ThreadInterruptedException tie)
                {
                    //interupted
                    tie.Dump();
                }
            });

            thread.Start();
            //等待线程执行指定的时长，如果到了指定的时长，线程还没有终止，就强制停止执行。
            if (!thread.Join(timeoutDelay))
            {
                thread.Interrupt();
            }
        }
    }
}
