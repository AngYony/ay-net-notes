using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.Sample
{

    //  创建处理异常相关的操作筛选器，注意：此处定义的是操作筛选器，而非异常筛选器
    public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
    {
        public int Order => int.MaxValue - 10; //设置优先级，保证其再所有筛选器之后处理OnActionExecuted方法

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //if (context.Exception is HttpResponseException exception)
            //{
            //    context.Result = new ObjectResult(exception.Value)
            //    {
            //        StatusCode = exception.Status,
            //    };
            //    //标注为已近被处理
            //    context.ExceptionHandled = true;
            //}
            if (context.Exception != null)
            {


                context.Result = new ObjectResult(context.Exception)
                {
                     
                };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
