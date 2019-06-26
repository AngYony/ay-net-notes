using Microsoft.Extensions.Configuration;
using System;


namespace JsonConfigSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("wy.json",false,true);

            var configuration= builder.Build();

            Console.WriteLine($"ClassNo:{ configuration["ClassNo"]}");
            Console.WriteLine(configuration["Students:0:name"]);
            Console.WriteLine(configuration["Students:1:name"]);
            Console.WriteLine(configuration["Students:2:name"]);

            Console.Read();
        }
    }
}


//该示例需要将wy.json文件的属性，“生成操作”设置为“内容”，“复制到输出目录”设置为“始终复制”
//这样每次重新生成项目时，wy.json文件才能同步输出到\bin\Debug\netcoreapp2.2目录下