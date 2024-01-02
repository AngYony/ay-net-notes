using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using My.ViewComponents.Study.Models;

namespace My.ViewComponents.Study.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDbContext dbContext;
        public HomeController(StudentDbContext _dbContext){
            dbContext = _dbContext;
            dbContext.Database.EnsureCreated();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Stu(){
            return ViewComponent("MyStudent", new { StudentNo = 4, StudentAddress = "" });
        }
    }
}