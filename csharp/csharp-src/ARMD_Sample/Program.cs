using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WhatsNewAttributes;

/// <summary>
/// 特性、反射、元数据、动态编程
/// </summary>
namespace ARMD_Sample
{
    class Program
    {
        static void Main(string[] args)
        {

            //Assembly assembly1 = Assembly.Load("WhatsNewAttributes");
            //Assembly assembly2 = Assembly.LoadFrom(@"D:\OctoberOcean\octocean26\Sunshine-Csharp\TestAssembly\bin\Debug\TestAssembly.dll");
            //string name = assembly1.FullName;
            //string name2 = assembly2.FullName;

            //Console.WriteLine(name);
            //Console.WriteLine(name2);

            //Type[] types = assembly1.GetTypes();

            //foreach(Type definedType in types)
            //{
            //    Console.WriteLine(definedType.Name);
            //}

            ////获取自定义特性的详细信息
            //Attribute [] definedAttributes = Attribute.GetCustomAttributes(assembly1);

            ClientApp.Run();


            Console.Read();
        }
    }
}
