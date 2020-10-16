using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_WebApplication.Models
{
    public class Student
    {
        public int Id{ get; set; }
        public string Name{ get;set; }
        public string Major{ get; set; }

    }


    

    //public class StudentRepository : IStudentRepository
    //{
    //    public Student GetStudent(int id)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Save(Student student)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}



}
