using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async_Sample
{
    class AsyncDemo
    {
        public static string Greeting(string name)
        {
            //等待3秒
            Task.Delay(3000).Wait(); //Wait方法用来等待之前的任务完成
            return "Hello," + name;
        }

        private static Task<string> GreetingAsync(string name)
        {
            return Task.Run<string>(() => { return Greeting(name); });
        }

        public async static void CallerWithAsync()
        {
            string result = await GreetingAsync("异步调用方法");
            Console.WriteLine(result);
        }

        private async static void CallerWithAsync2()
        {
            Console.WriteLine(await GreetingAsync(".net"));
        }


        public static void CallerWithContinuationTask()
        {
            Task<string> t1 = GreetingAsync("异步调用方法");
            t1.ContinueWith(t =>
            {
                string result = t.Result;
                Console.WriteLine(result);
            });
        }


        public async static void MultipleAsyncMehtods()
        {
            string s1 =await GreetingAsync("Mul1");
            string s2 =await GreetingAsync("Mul2");
            Console.WriteLine("Mul:" + s1 + " " + s2);
        }


        public async static void MultipleAsyncMethodsWithCombinators1()
        {
            Task<string> t1 = GreetingAsync("mulA");
            Task<string> t2 = GreetingAsync("mulB");
            await Task.WhenAll(t1, t2);
            Console.WriteLine("结果：" + t1.Result + "  " + t2.Result);
        }


        private async static void MultipleAsyncMethodsWithCombinators2()
        {
            Task<string> t1 = GreetingAsync("AAAA");
            Task<string> t2 = GreetingAsync("BBBB");
            string[] result = await Task.WhenAll(t1, t2);
            Console.WriteLine("结果:" + result[0] + "  " + result[1]);
        }


        //定义一个委托
        private static  Func<string, string> greetingInvoker = Greeting;

        //模拟异步模式
        private static IAsyncResult BeginGreeting(string name,AsyncCallback callback, object state)
        {
            return greetingInvoker.BeginInvoke(name, callback, state);
        }
        //该方法返回来自于Greeting的结果
        private static string EndGreeting(IAsyncResult ar)
        {
            return greetingInvoker.EndInvoke(ar);
        }


        public static async void ConvertingAsyncPattern()
        {
            string s = await Task<string>.Factory.FromAsync<string>(BeginGreeting, EndGreeting, "测试", null);
            Console.WriteLine(s);
        }

    }
}
