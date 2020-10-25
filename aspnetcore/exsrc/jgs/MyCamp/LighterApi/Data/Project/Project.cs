using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighterApi.Data.Project
{
    /// <summary>
    /// 项目
    /// </summary>
    public class Project:Entity
    {
        public string Title{ get; set; }
        public string Superviosr{ get; set; }
        public DateTime StartDate{ get; set; }
        public DateTime EndDate{ get; set; }
        public string PlanId{ get; set; }
    }
}
