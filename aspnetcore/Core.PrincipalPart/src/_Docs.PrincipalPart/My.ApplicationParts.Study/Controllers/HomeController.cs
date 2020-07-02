using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace My.ApplicationParts.Study.Controllers
{
    public class HomeController:Controller
    {
        public IActionResult Index(){
            return View();
        }
    }
}
