using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace S07_TaskScheduler {

    internal class Program {

        private static void Main(string[] args) {
            var scheduler = new CustomTaskScheduler();
            for (int i = 0; i < 100; i++) {
                var task = Task.Factory.StartNew(() => {
                    Console.WriteLine($"线程Id：{Environment.CurrentManagedThreadId}");
                }, CancellationToken.None, TaskCreationOptions.None, scheduler);
            }
            Console.ReadLine();
        }
    }

    public class CustomTaskScheduler : TaskScheduler {

        //定义一个Thread执行所有的Task
        private Thread th = null;

        private BlockingCollection<Task> collection = new BlockingCollection<Task>();

        public CustomTaskScheduler() {
            th = new Thread(() => {
                foreach (var task in collection.GetConsumingEnumerable()) {
                    TryExecuteTask(task);
                }
            });
            th.Start();
        }

        protected override IEnumerable<Task> GetScheduledTasks() {
            return collection.ToArray();
        }

        protected override void QueueTask(Task task) {
            collection.Add(task);
        }

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued) {
            throw new NotImplementedException();
        }
    }
}