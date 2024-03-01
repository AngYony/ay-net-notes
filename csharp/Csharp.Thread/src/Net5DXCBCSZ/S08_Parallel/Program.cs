using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace S08_Parallel {
    class Program {
        static void Main(string[] args) {

            Sample4();
        }

        static void Email() {
            Console.WriteLine("开始发送邮件...");
            Thread.Sleep(2000);
            Console.WriteLine("发送邮件耗时2秒");
        }
        static void SMS() {
            Console.WriteLine("开始发送短信...");
            Thread.Sleep(1000);
            Console.WriteLine("发送短信耗时1秒");

        }
        static void WeChat() {
            Console.WriteLine("开始发送微信...");
            Thread.Sleep(3000);
            Console.WriteLine("发送微信耗时3秒");
        }

        static void Sample1() {
            Parallel.Invoke(Email, SMS, WeChat);
            Console.WriteLine("执行完毕");
            Console.ReadLine();
        }

        static void Sample2() {
            Parallel.For(0, 100, new ParallelOptions() { MaxDegreeOfParallelism = 2 },
            (index) => {
                Console.WriteLine(index);
            });
        }

        static void Sample3() {
            var dic = new Dictionary<int, int>() { [1] = 10, [2] = 20, [3] = 30 };
            Parallel.ForEach(dic, (item) => {
                Console.WriteLine(item.Key + "：" + item.Value);
            });
            Console.ReadLine();
        }

        static void Sample4() {
            // 计算1~100的总和
            var total = 0;
            Parallel.For(1, 100, () => 0,
            (num, state, sum) => {
                return num + sum;
            }, sum => {
                Interlocked.Add(ref total, sum);
            });
            Console.WriteLine($"total={total}");
        }
    }
}
