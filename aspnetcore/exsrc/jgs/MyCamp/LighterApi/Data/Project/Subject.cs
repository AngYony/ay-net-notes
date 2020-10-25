using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighterApi.Data.Project
{
    public class Subject:Entity
    {
        public string Title{ get; set; }
        public string Content{ get; set; }
        public IList<SubjectProject> SubjectProjects { get; set; }
    }
}
