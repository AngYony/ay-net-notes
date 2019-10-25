using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;
namespace SpecialCollections_Sample
{
    class ImmutableDemo
    {
        public static void Run()
        {
            
            ImmutableArray<string> a1= ImmutableArray.Create<string>();
            ImmutableArray<string> a2= a1.Add("java");
            ImmutableArray<string> a3 = a2.Add("c#").Add("python").Add("php");

            
        }
    }
}
