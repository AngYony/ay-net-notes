using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace S09_APM_EAP {

    internal class Program {

        private static void Main(string[] args) {
           var length= Sample4().Result;
            Console.WriteLine($"当前的Length={length}");  

        }

        // 原生获取文件大小
        private static void Sample1() {
            FileStream fileStream = new FileStream(Environment.CurrentDirectory + "//1.txt", FileMode.Open);
            byte[] bytes = new byte[fileStream.Length];
            fileStream.BeginRead(bytes, 0, bytes.Length, (IAsyncResult ir) => {
                var length = (ir.AsyncState as FileStream).EndRead(ir);
                Console.WriteLine($"当前的Length={length}");
            }, fileStream);

            Console.Read();
        }

        //
        private static void Sample2() {
            FileStream fileStream = new FileStream(Environment.CurrentDirectory + "//1.txt", FileMode.Open);

            byte[] bytes = new byte[fileStream.Length];

            var task = Task.Factory.FromAsync(fileStream.BeginRead, fileStream.EndRead, bytes, 0, bytes.Length, fileStream);
            Console.WriteLine($"当前的Length={task.Result}"); //阻塞直到获取了结果

            Console.Read();
        }

        private static Task<int> GetFileLengthAsync() {
            TaskCompletionSource<int> source = new TaskCompletionSource<int>();
            FileStream fileStream = new FileStream(Environment.CurrentDirectory + "//1.txt", FileMode.Open);

            byte[] bytes = new byte[fileStream.Length];

            fileStream.BeginRead(bytes, 0, bytes.Length, (IAsyncResult ir) => {
                var length = (ir.AsyncState as FileStream).EndRead(ir);
                // 将结果写入到source中
                source.SetResult(length);
            }, fileStream);
            // return返回的时候，上述方法还没有被执行，因此方法名标记为了异步
            return source.Task;
        }
        // 使用WebClient判断HTML长度
        static void Sample3() {
            WebClient client = new WebClient();
            client.DownloadStringCompleted += (sender, e) => {
                var length = e.Result.Length;
                Console.WriteLine("当前的Length=" + length);
            };

            client.DownloadStringAsync(new Uri("http://cnblogs.com"));
            Console.Read();
        }


        static Task<int> Sample4() {
            TaskCompletionSource<int> source = new TaskCompletionSource<int>();
            WebClient client = new WebClient();
            client.DownloadStringCompleted += (sender, e) => {
                if (e.Error != null) {
                    source.SetException(e.Error);
                }
                else{
                    var length = e.Result.Length;
                    source.SetResult(length);
                }
            };

            client.DownloadStringAsync(new Uri("http://cnblogs.com"));
            return source.Task;
        }


    }
}