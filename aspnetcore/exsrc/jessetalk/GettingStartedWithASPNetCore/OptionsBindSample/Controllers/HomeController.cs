using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptionsBindSample.Controllers
{
    public class HomeController:Controller
    {
        private readonly MyClass _myClass;  

        public HomeController(IOptions<MyClass> options){
            _myClass= options.Value;
        }

        public IActionResult Index(){
            return View(_myClass);
        }

        public IActionResult Inject(){
            return View();
        }
    }
}
