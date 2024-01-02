using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.MVC.Study.DI
{
    interface IDateTime
    {
        DateTime Now { get; }
    }
}
