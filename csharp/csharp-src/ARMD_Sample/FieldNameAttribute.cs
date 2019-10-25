using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMD_Sample
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple=false, Inherited=false)]
    class FieldNameAttribute : Attribute
    {
        public string Comment { get; set; }
        private string _fileName;
        public FieldNameAttribute(string fileName)
        {
            _fileName = fileName;
        }
    }
}
