using Microsoft.AspNetCore.Mvc;

namespace My.ApplicationParts.Study
{
    [WyControllerNameConvention]
    public class WyController<T> : Controller
    {
        public IActionResult Index()
        {
            return Content(typeof(T).Name);
        }
    }
}