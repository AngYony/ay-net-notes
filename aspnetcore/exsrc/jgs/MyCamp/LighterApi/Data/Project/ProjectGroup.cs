using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighterApi.Data.Project
{
    /// <summary>
    /// 项目分组
    /// </summary>
    public class ProjectGroup:Entity
    {
        public string Name{ get; set; }
        public string ProjectId{ get; set; }
        //public List<Member> Superviosr{ get; set; }
         
    }
}
