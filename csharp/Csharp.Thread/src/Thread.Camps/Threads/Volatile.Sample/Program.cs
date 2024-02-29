namespace Volatile.Sample
{
    internal class Program
    {
        private static volatile int sum = 0;
        ////单独执行一次，sum值为1000000
        static void AddOne()
        {
            for (int i = 0; i < 100_0000; i++)
            {
                sum += 1;
            }
        }
        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                new Thread(AddOne).Start();
            }
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Console.WriteLine("sum = " + sum);
            Console.ReadKey();
        }
    }
}
