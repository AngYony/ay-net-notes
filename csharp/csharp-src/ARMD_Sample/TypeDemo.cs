using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VectorClass;

namespace ARMD_Sample
{
    class TypeDemo
    {
        public static void Run()
        {
            //Type t = typeof(double);

            //Console.WriteLine(t.IsAbstract);
            //Console.WriteLine(t.IsClass);
            //Console.WriteLine(t.IsArray);
            //Console.WriteLine(t.IsEnum);
            //Console.WriteLine(t.IsInterface);
            //Console.WriteLine(t.IsPointer);
            ////一种预定义的基元数据类型
            //Console.WriteLine(t.IsPrimitive);
            //Console.WriteLine(t.IsPublic);
            //Console.WriteLine(t.IsSealed);
            //Console.WriteLine(t.IsValueType);


            Type t2 = typeof(Vector);
            Assembly containingAssembly = Assembly.GetAssembly(t2);

            t2.GetTypeInfo();
            
        }
    }
}
