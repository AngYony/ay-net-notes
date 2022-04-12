using MVC_WebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_WebApplication.DataRepositories
{
    public interface IStudentRepository
    {
        Student GetStudent(int id);

        void Save(Student student);
    }
}
