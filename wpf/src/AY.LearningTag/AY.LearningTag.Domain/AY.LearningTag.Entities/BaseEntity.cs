using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AY.LearningTag.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreateBy { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public string UpdateBy { get; set; }

    }
}
