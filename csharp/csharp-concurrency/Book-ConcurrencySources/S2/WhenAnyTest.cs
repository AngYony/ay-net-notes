using System.Net.Http;
using System.Threading.Tasks;

namespace S2
{
    public class WhenAnyTest
    {
        //返回第一个响应的URL的数据长度
        private static async Task<int> FirstRespondingUrlAsync(string urlA, string urlB)
        {
            var httpClient = new HttpClient();

            //并发的开始两个下载任务
            Task<byte[]> downloadTaskA = httpClient.GetByteArrayAsync(urlA);
            Task<byte[]> downloadTaskB = httpClient.GetByteArrayAsync(urlB);

            //等待任意一个任务完成
            //返回Task<Task<byte[]>>，表示提供的任务之一已完成的任务。 返回任务的结果是完成的任务。
            Task<byte[]> completedTask = await Task.WhenAny(downloadTaskA, downloadTaskB);

            //返回从URL得到的数据的长度
            byte[] data = await completedTask;
            return data.Length;
        }
    }
}