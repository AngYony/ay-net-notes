using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.Sample
{
    public class HttpResponseException:Exception
    {
        public int Status { get; set; } = 500;
        public object Value{ get; set; }
    }
}
