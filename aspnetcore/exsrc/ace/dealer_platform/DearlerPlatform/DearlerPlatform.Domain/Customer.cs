using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Domain
{
    public class Customer : BaseEntity
    {
        public new int Id { get; set; }

        public string CustomerNo { get; set; }
        public string  CustomerName { get; set; }
    }
}
