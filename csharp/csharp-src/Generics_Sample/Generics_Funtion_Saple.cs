using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics_Sample
{
    class Generics_Funtion_Saple
    {
        void Swap<T>(ref T x,ref T y)
        {
            T temp;
            temp = x;
            x = y;
            y = temp;
        }

        public void Run()
        {
            int a = 1, b = 2;
            Swap<int>(ref a, ref b);
            //C#编译器会通过调用该方法来获取参数的类型，所以不需要把泛型类型赋予方法调用，可简化为下述语句
            Swap(ref a, ref b);
        }
    }
}
