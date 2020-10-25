using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        
         
        //外键，指向主表Project
        public string ProjectId{ get; set; }


        //public List<Member> Superviosr{ get; set; }

        public Project Project{ get; set; }

        public IList<Member> Members{ get; set; }
         
    }
}
