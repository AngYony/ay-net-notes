using Microsoft.AspNetCore.Mvc;
using My.MvcFilters.Study.Filters;

namespace My.MvcFilters.Study.Controllers
{
    [AddHeader("Author", "Smallz")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            return Content("测试");
        }

        [ShortCircuitingResourceFilter]
        public IActionResult SomeResource()
        {
            return Content("成功访问到标头资源");
        }

        [AddHeaderWithFactory]
        public IActionResult HeaderWithFactory()
        {
            return Content("AddHeaderWithFactory");
        }

        [ServiceFilter(typeof(AddHeaderFilterWithDi))]
        public IActionResult ServiceFilterTest()
        {
            return Content("ServiceFilter应用");
        }

        [TypeFilter(typeof(LogConstantFilter),
                Arguments = new object[] { "Method 'Hi' called" })]
        public IActionResult Hi(string name)
        {
            return Content("Hi," + name);
        }

        [SampleActionFilter]
        public IActionResult HiFactory(){
            return Content("在属性上实现IFilterFactory");
        }
    }
}