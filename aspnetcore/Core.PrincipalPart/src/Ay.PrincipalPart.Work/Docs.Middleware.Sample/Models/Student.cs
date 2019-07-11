using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Middleware.Sample.Models
{
    public class Student
    {
        public Student()
        {

        }
        public Student(string Name)
        {
            this.Name = Name;
        }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
