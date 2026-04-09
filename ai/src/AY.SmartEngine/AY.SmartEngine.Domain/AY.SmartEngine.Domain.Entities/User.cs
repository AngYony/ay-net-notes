using System;
using System.Collections.Generic;
using System.Text;

namespace AY.SmartEngine.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string Username { get; set; } //创建对象必须是赋值
        public string? Email { get; set; }
    }
}
