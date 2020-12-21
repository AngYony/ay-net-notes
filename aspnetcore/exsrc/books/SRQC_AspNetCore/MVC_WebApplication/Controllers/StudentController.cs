using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MVC_WebApplication.DataRepositories;
using MVC_WebApplication.Models;

namespace MVC_WebApplication.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public ActionResult Details(int id)
        {
            Student student = _studentRepository.GetStudent(id);
            return View(student);
            //return new ObjectResult(student);
        }

        public ActionResult JSONDetails(int id)
        {
            Student student = _studentRepository.GetStudent(id);
            
            return new ObjectResult(student);
        }



        public IActionResult Index()
        {
            return View();
        }
    }
}
