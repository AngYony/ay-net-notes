using Microsoft.Extensions.Configuration;
using System;


namespace JsonConfigSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
            .AddJsonFile("wy.json");

            var configuration= builder.Build();

            Console.WriteLine($"ClassNo:{ configuration["ClassNo"]}");
            Console.WriteLine(configuration["Students:0:name"]);
            Console.WriteLine(configuration["Students:1:name"]);
            Console.WriteLine(configuration["Students:2:name"]);

            Console.Read();
        }
    }
}
