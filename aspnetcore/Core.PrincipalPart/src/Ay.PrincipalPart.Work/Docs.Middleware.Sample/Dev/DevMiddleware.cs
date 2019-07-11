using Docs.Middleware.Sample.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Middleware.Sample.Dev
{
    public class DevMiddleware
    {

        private readonly RequestDelegate _next;

        public DevMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        //定义处理任务
        public async Task InvokeAsync(HttpContext context)
        {
            //此处进行一些操作

            //例如获取查询字符串参数
            var query = context.Request.Query["q"];
            if (!string.IsNullOrWhiteSpace(query))
            {
                MyClass.StudentB = new Student(query);
                //其他的一些操作
            }

            //最后必不可少的代码
            await _next(context);
        }
    }
}
