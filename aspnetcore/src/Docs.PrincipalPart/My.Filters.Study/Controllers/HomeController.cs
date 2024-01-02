using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using My.Filters.Study.Filters;

namespace My.Filters.Study.Controllers
{
    [AddHeader("Author", "ceshi")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}