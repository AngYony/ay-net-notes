using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.MVC.Study.DI
{
    public class SystemDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
