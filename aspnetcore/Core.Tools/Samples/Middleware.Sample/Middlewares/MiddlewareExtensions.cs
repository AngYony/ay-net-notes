using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Sample.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static void UseTest(this IApplicationBuilder app)
        {
            //Use中的内容会在每次请求时都会执行
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/weatherforecast")
                {
                    //context.Response.StatusCode = 500;
                    await next();
                }
                else
                {
                    await next();
                }
            });
        }
    }
}
