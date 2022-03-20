using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yang.API.Models
{
    public class Student : IStudentService
    {
        public string Code { get; set; }

        public Student()
        {
            Code = $"Student:{ Guid.NewGuid().ToString()}";
        }
    }
}
