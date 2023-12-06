using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DearlerPlatform.Domain
{
    public class CustomerPwd : BaseEntity
    {
        public new int Id { get; set; }

        public string CustomerNo { get; set; }

        public string Password { get; set; }
    }
}
