using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.RazorRoute.Study.Filters
{
    public class EmptyFilter : IActionFilter
    {
       
        public void OnActionExecuted(ActionExecutedContext context)
        {
       
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
       
        }
    }
}
