using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace My.MVC.Study.Controllers
{
    public class HomeController : Controller
    {
        [ViewData]
        public string Message { get; set; }

         
        public IActionResult Index()
        {
            Message = "Hello，MVC";
            return View();
        }
    }
}