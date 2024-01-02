using Microsoft.AspNetCore.Mvc.Filters;

namespace My.MvcFilters.Study.Filters
{
    public class SampleActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //在每个操作方法返回之后调用
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //在调用每个操作方法之前调用
        }
    }
}