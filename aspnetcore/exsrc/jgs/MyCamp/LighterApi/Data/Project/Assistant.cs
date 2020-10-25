using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LighterApi.Data.Project
{
    /// <summary>
    /// 助教
    /// </summary>
    public class Assistant:Entity
    {
        public string MemberId{ get; set; }   
        public string ProjectGroupId{ get; set; }


     }
}
