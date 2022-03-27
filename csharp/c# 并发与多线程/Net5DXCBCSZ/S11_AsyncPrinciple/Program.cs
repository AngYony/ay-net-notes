using Microsoft.Win32.SafeHandles;
using System;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace S11_AsyncPrinciple {
    class Program {
        static void Main(string[] args) {
            GetContentLengthAsync("http://cnblogs.com");
            Console.WriteLine($"主线程：{Environment.CurrentManagedThreadId}");
            Console.Read();
        }

        static async Task<int> GetContentLengthAsync(string url) {
            using (HttpClient client = new HttpClient()) {
                var content = await client.GetStringAsync(url);
                Console.WriteLine($"当前线程：{Environment.CurrentManagedThreadId}");
                return content.Length;
            }
        }
    }

    //public class IOCP
    //{
    //    [DllImport("kernel32.dll",CharSet=CharSet.Auto,SetLastError =true)]
    //    public static extern SafeFileHandle CreateIoCompletionPort(IntPtr FileHandle,IntPtr ExistingCompletionPort,)
    //}
}
