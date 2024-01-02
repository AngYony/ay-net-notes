using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.RazorRoute.Study.Factories
{
    public class AddHeaderWithFactory : IFilterFactory
    {
        public bool IsReusable => false;

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            return new AddHeaderFilter();
        }

        private class AddHeaderFilter : IResultFilter
        {
            public void OnResultExecuted(ResultExecutedContext context)
            {
                
            }

            public void OnResultExecuting(ResultExecutingContext context)
            {
                context.HttpContext.Response.Headers.Add(
                    "FilterFactoryHeader",
                    new string[]
                    {
                        "Filter Factory Header Value 1",
                        "Filter Factory Header Value 2"
                    });
            }
        }
    }
}
