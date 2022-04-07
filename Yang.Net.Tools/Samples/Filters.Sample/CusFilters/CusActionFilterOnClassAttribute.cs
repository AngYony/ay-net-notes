using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters.Sample.CusFilters
{
    // 自定义操作过滤器：用于模型验证和日志存储 
    public class CusActionFilterOnClassAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("在类上执行：OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

           var logger= context.HttpContext.RequestServices.GetService<ILogger<CusActionFilterOnClassAttribute>>();
            var path = context.HttpContext.Request.Path.Value;
            var controller = context.Controller.ToString();
            var action = context.RouteData.Values["action"];
            var arguments = string.Join(",", context.ActionArguments);
            logger.LogInformation($"访问的路由：{path}，控制器是：{controller}，行为是：{action}，参数是：{arguments}");
             
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            Console.WriteLine("在类上执行：OnActionExecuting");
        }
    }
}
