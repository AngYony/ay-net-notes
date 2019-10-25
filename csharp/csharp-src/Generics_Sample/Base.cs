using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics_Sample
{
    public class Base<T> { }
    public class Derived<T> : Base<T> { }
    public class Derived_2<T> : Base<string> { }
}
