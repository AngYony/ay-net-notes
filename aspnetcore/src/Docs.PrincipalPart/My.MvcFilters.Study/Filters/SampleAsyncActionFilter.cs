using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace My.MvcFilters.Study.Filters
{
    public class SampleAsyncActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(
        ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //调用之前
            var resultContext = await next();
            //调用之后
        }
    }
}