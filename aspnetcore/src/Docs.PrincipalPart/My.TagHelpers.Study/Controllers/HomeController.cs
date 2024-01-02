using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using My.TagHelpers.Study.Component;
using My.TagHelpers.Study.Models;

namespace My.TagHelpers.Study.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITagHelperComponentManager _tagHelperComponentManager;

        public HomeController(ITagHelperComponentManager tagHelperComponentManager)
        {
            _tagHelperComponentManager = tagHelperComponentManager;
        }

        public IActionResult Index()
        {
            return View();
            return RedirectToAction("Component");
        }

        [Route("Tag/{wy?}/{smallz?}")]
        public IActionResult Tag()
        {
            TagViewModel tagmodel = new TagViewModel
            {
                Address = new AddressViewModel { AddressLine = "beijing" },
                Colors = new List<string> { "Red", "Blue", "Yellow" },
                Email = "wy@163.com",
                Password = "WW",
                Countries = new List<SelectListItem>{
                 new SelectListItem{ Value="1", Text="One"},
                 new SelectListItem{ Value="2", Text="Two"},
                 new SelectListItem{ Value="3", Text="Three"}
                 },
                Country = "3",
                EnumCountry = CountryEnum.Two
            };

            return View(tagmodel);
        }

        public IActionResult Component()
        {
            return View();
        }

        public IActionResult Component2()
        {
            _tagHelperComponentManager.Components.
               Add(new AddressTagHelperComponent("<h1>AAAAAAAAA</h1>", 1));
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        [Route("/Home/Test2", Name = "Custom")]
        public string Test2()
        {
            return "Test2";
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(string wy)
        {
            return View();
        }
    }
}