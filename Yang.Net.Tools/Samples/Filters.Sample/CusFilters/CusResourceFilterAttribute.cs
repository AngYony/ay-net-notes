using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters.Sample.CusFilters
{
    // 资源过滤器：资源短路，用于缓存，可参考官方用法，可使用Redis替换IMemoryCache.
    public class CusResourceFilterAttribute : Attribute, IResourceFilter
    {
        private readonly IMemoryCache memoryCache;
        public CusResourceFilterAttribute(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
        }




        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            var path = context.HttpContext.Request.Path.Value;
            if (context.Result != null)
            {
                var value = (context.Result as ObjectResult)?.Value?.ToString();
                if (value != null)
                {
                    memoryCache.Set(path, value,TimeSpan.FromHours(1));
                }
            }
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string actionName = ((ControllerActionDescriptor)context.ActionDescriptor).ActionName;
            var path = context.HttpContext.Request.Path.Value;
            if (memoryCache.TryGetValue(path, out var value))
            {
                //为Context.Result设置的值，将会直接被返回，不会进入到控制器中    
                context.Result = new ContentResult
                {
                    Content = value.ToString()
                };
            }

        }
    }
}