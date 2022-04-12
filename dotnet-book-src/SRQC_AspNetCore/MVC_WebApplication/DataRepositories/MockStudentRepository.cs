using MVC_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_WebApplication.DataRepositories
{
    public class MockStudentRepository : IStudentRepository
    {
        
        public Student GetStudent(int id)
        {
            return new Student
            {
                Id = 1,
                Name = "wy",
                Major = "AAA"
            };
        }

        public void Save(Student student)
        {
            
        }
    }
}
