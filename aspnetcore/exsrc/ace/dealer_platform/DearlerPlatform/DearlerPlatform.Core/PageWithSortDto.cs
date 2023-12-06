using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Core
{
    public class PageWithSortDto
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;

        public string Sort { get; set; }
        public OrderType OrderType { get; set; }
    }

    public enum OrderType
    {
        Asc,
        Desc
    }
}
