using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace My.MvcFilters.Study.Filters
{
    public class AddHeaderFilterWithDi : IResultFilter
    {
        private ILogger _logger;

        public AddHeaderFilterWithDi(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AddHeaderFilterWithDi>();
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var headerName = "WyOnResultExecuting";
            context.HttpContext.Response.Headers.Add(headerName, new string[] { "AAA", "BBB" });
            _logger.LogInformation($"Header added:{headerName}");
        }
    }
}