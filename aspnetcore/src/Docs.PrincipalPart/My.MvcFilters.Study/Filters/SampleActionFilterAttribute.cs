using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace My.MvcFilters.Study.Filters
{
    public class SampleActionFilterAttribute : TypeFilterAttribute
    {
        public SampleActionFilterAttribute() : base(typeof(SampleActionFilterImpl))
        {
        }

        private class SampleActionFilterImpl : IActionFilter
        {
            private readonly ILogger _logger;

            public SampleActionFilterImpl(ILoggerFactory _loggerFactory)
            {
                _logger = _loggerFactory.CreateLogger<SampleActionFilterAttribute>();
            }

            public void OnActionExecuted(ActionExecutedContext context)
            {
                _logger.LogInformation("操作完成");
            }

            public void OnActionExecuting(ActionExecutingContext context)
            {
                _logger.LogInformation("开始执行操作");
            }
        }
    }
}