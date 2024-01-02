using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.Filters.Study.Filters
{
    public class SampleAsyncPageFilter : IAsyncPageFilter
    {
        private readonly ILogger _logger;

        public SampleAsyncPageFilter(ILogger logger)
        {
            this._logger = logger;
        }
        public async Task OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            _logger.LogDebug("全局过滤器方法：OnPageHandlerSelectionAsync，被调用 【WY01】");
            await Task.CompletedTask;
        }


        public async Task OnPageHandlerExecutionAsync(PageHandlerExecutingContext context,
            PageHandlerExecutionDelegate next)
        {
            _logger.LogDebug("全局过滤器方法：OnPageHandlerExecutionAsync ，被调用 【WY02】");
            await next.Invoke();
        }


    }
}
