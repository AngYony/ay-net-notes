using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
