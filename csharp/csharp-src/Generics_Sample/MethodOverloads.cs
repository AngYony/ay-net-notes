using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics_Sample
{
    public class MethodOverloads
    {
        public void Foo<T>(T obj)
        {
            Console.WriteLine($"Foo<T>(T obj),obj type:{obj.GetType().Name}");
        }

        public void Foo(int x)
        {
            Console.WriteLine("Foo(int x)");
        }

        public void Foo<T1, T2>(T1 obj1, T2 obj2)
        {
            Console.WriteLine($"Foo<T1,T2>(T1 obj1,T2 obj2);  {obj1.GetType().Name}\t {obj2.GetType().Name}");
        }

        public void Foo<T>(int obj1,T obj2)
        {
            Console.WriteLine($"Foo<T>(int obj1,int obj2);{obj2.GetType().Name}");
        }

        public void Bar<T>(T obj)
        {
            Foo(obj);
        }
    }
}
