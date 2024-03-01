namespace Tasks.Factory.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var task4 = Task.Factory.StartNew((obj) => {
                Console.WriteLine($"当前任务的线程的ID：{Environment.CurrentManagedThreadId}，传入的值为：{obj}");
            }, "张三");
            Console.WriteLine($"task.id={task4.Id}，task.status={task4.Status}");
        }
    }
}
