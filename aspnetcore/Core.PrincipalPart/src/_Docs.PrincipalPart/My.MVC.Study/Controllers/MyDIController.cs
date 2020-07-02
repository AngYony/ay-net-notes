using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using My.MVC.Study.DI;

namespace My.MVC.Study.Controllers
{
    public class MyDIController : Controller
    {
        public IActionResult Index()
        {
            SystemDateTime dt = new SystemDateTime();
            
            return View();
        }
    }
}