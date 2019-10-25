using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generics_Sample
{
    public class Query<TRequest, TResult> { }
    public class StringQuery<TRequest> : Query<TRequest, string> { }
}
