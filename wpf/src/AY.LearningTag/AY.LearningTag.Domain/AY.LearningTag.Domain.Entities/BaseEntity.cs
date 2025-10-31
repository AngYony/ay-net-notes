using AY.LearningTag.Domain.Abstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Domain.Entities
{
    public class BaseEntity : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreateBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdateBy { get; set; }

    }
}
