using Microsoft.AspNetCore.Mvc;

namespace My.Area.Study.Areas.SmallZ.Controllers
{
    [Area("SmallZ")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }
    }
}