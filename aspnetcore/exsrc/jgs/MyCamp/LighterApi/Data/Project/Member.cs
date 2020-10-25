using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighterApi.Data.Project
{
    /// <summary>
    /// 项目成员
    /// </summary>
    public class Member:Entity
    {
         
        public string Progress { get; set; }
        public int ProjectId { get; set; }
    }
}
