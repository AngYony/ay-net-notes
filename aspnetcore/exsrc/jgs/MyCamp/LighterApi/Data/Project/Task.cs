using LighterApi.Share;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighterApi.Data.Project
{
    /// <summary>
    /// 任务
    /// </summary>
    public class Task:Entity
    {
        public string Title{ get; set; }
        public int SectionId{ get; set; }
        public string Description{ get; set; }
        public int ProjectId{ get; set; }

        public string MemberId{ get; set; }

        public EnumTaskStatus Status{ get; set; }
        
         
        
    }
}
