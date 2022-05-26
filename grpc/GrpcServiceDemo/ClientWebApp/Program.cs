using Grpc.Net.Client;
using GrpcServiceDemo;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClientWebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var channel = GrpcChannel.ForAddress();
            //var client = new Greeter.GreeterClient(channel);


            //var reply = client.SayHello(new HelloRequest()
            //{
            //    Name = "ËïÎò¿Õ",
            //    Age = 500

            //});

            //Console.WriteLine(reply.Message);


            //var weihudaiclient = new WeiHuDai.WeiHuDaiClient(channel);
            //var res = weihudaiclient.GetWeiHuDaiList(new WeiHuDaiListFrom { KJDM = "BLG" });

            //foreach (var item in res.WhdDto)
            //{
            //    Console.WriteLine($"Ãû³Æ£º{item.Name},³¤¶È:{item.ChangDu}");
            //}



            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
