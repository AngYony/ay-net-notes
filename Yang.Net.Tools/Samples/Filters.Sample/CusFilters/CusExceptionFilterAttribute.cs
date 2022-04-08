using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters.Sample.CusFilters
{
    //自定义异常过滤器
    /*
     * 注意：
     * 授权过滤器中的异常无法捕获
     * 资源过滤器中异常无法捕获
     * 可以捕获Action过滤器中的异常
     * 如果想要捕获全部的异常，必须使用中间件才可以。
     */
    public class CusExceptionFilterAttribute : Attribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.Result = new ContentResult
            {
                Content = context.Exception.ToString()
            };
        }
    }
}
