using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighterApi.Data.Project
{
    public class SubjectProject : Entity
    {
        public string ProjectId { get; set; }
        public Project Project { get; set; }
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
