using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters.Sample.CusFilters
{
    // 资源过滤器：资源短路
    public class CusResourceFilterAttribute : Attribute, IResourceFilter
    {

        static IActionResult? result = null;
        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            result = context.Result;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string actionName = ((ControllerActionDescriptor)context.ActionDescriptor).ActionName;
            if ("DoHomework".Equals(actionName))
            {
                if (result != null)
                    //为Context.Result设置的值，将会直接被返回，不会进入到控制器中
                    context.Result = result;
            }
        }
    }
}