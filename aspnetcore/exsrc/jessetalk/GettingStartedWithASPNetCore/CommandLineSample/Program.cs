using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace CommandLineSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new Dictionary<string, string>{
            { "name","smallz" },
            {"age","18" }
            };


            var builder = new ConfigurationBuilder()
            .AddInMemoryCollection(settings)
            .AddCommandLine(args); //命令行的优先级高于内存集合

            var configuration = builder.Build();

            Console.WriteLine($"name:{configuration["name"]}");
            Console.WriteLine($"age:{configuration["age"]}");

            Console.ReadLine();
        }
    }
}
