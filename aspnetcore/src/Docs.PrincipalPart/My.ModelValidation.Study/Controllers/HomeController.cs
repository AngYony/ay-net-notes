using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using My.ModelValidation.Study.ViewModels;

namespace My.ModelValidation.Study.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Save(StudentViewModel student)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", student);
            }
            return View("Index", student);
            //return RedirectToAction(actionName: nameof(Index));
        }


        [AcceptVerbs("Get", "Post")]
        public IActionResult VerifyData([Microsoft.AspNetCore.Mvc.ModelBinding.BindRequired,FromQuery] string StudentName, int Age, DateTime Birthday)
        {

            if (!StudentName.Equals("smallz"))
            {
                return Json("值必须为smallz");
            }
            return Json(true);
        }

    }
}