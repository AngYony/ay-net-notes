using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Options.Sample.Models
{
    public class MyOptionsWithDelegateConfig
    {
        public MyOptionsWithDelegateConfig(){
            Option1 = "value1_from_ctor";
        }


        public string Option1 { get; set; }

        public int Option2 { get; set; } = 5;
    }
}
