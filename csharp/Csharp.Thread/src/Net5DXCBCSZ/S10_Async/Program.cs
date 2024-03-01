using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace S10_Async {
    class Program {
        static void Main(string[] args) {
            GetContentLengthAsync("http://cnblogs.com");
            Console.WriteLine($"主线程：{Environment.CurrentManagedThreadId}");
            Console.Read();
        }

        static async Task<int> GetContentLengthAsync(string url){
            using(HttpClient client=new HttpClient()){
                var content =await client.GetStringAsync(url);
                Console.WriteLine($"当前线程：{Environment.CurrentManagedThreadId}");
                return content.Length;
            }
        }
    }
}
