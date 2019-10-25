using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMD_Sample
{
    class AttributeDemo
    {
        [FieldName("SocialSecurityNumber",Comment ="测试")]
        public string SocialSecurityNumber { get; set; }
    }
}
