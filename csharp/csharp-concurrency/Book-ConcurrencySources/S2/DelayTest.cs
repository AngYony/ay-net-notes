using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace S2
{
    class DelayTest
    {
        /// <summary>
        /// 简单超时功能，如果服务在3秒内没有响应，就返回null
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        static async Task<string> DownloadStrtingWithTimeout(string url)
        {
            using (var client = new HttpClient())
            {
                var downloadTask = client.GetStringAsync(url); //返回Task<string>
                var timeoutTask = Task.Delay(3000); //返回Task

                var comletedTask = await Task.WhenAny(downloadTask, timeoutTask);
                if (comletedTask == timeoutTask)
                    return null;
                return await downloadTask;
            }
        }
    }
}
