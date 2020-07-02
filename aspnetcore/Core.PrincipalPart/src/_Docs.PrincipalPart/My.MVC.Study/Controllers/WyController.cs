using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace My.MVC.Study.Controllers
{
    public class WyController : Controller
    {
         
        public IActionResult Index()
        {
            return View();
        }

        //[Route("[action]")]
        public IActionResult MyTest()
        {
            return View();
        }
        [MyCusRoute]
        public IActionResult MyRoute()
        {
            return View();
        }

         
    }
}