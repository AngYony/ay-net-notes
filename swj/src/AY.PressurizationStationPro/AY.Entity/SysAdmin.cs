using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.Entity
{
    public class SysAdmin
    {
        public int LoginId { get; set; }
        public string LoginName { get; set; }
        public string LoginPwd { get; set; }
        public RoleName RoleName { get; set; }

        public DateTime LoginTime { get; set; } = DateTime.Now;
    }
}
