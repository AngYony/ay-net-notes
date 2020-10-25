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
    public class Project : Entity
    {
        public string Title { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        public string SuperviosrId { get; set; }
        
        public string PlanId { get; set; }

        public IList<ProjectGroup> Groups { get; set; }
        public IList<Task> Tasks{ get; set; }
        public IList<SubjectProject> SubjectProjects { get; set; }
    }
}
