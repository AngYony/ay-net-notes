using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string Username { get; set; }
        public string? Email { get; set; }
        public string Url { get; set; }
    }
}
