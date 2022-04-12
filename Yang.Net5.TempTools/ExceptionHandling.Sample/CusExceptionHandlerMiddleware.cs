using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.Sample
{
    public class CusExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<CusExceptionHandlerMiddleware> logger;

        public CusExceptionHandlerMiddleware(RequestDelegate next, ILogger<CusExceptionHandlerMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        //约定的方法，必不可少，一旦缺少将会引发异常，因此一般建议将其定义为接口方法，然后实现该方法
        public async Task InvokeAsync(HttpContext context)
        {


            try
            {
                await next(context); //必不可少
            }
            catch (Exception exception)
            {
                //已经捕获了异常
                await NewMethod(context, exception);

            }
            throw new Exception("再次抛出异常，触发/error-local-development");
            //如果在中间件中直接为 Response 做输出操作，将会阻止其他处理程序的执行.

            async Task NewMethod(HttpContext context, Exception exception)
            {
                return; //如果去掉return，将会执行下述的context.Response.Write操作，将会直接输出结果，而不跳转到异常处理路由页
                //在中间件中为Response的ContentType设置值时，需要注意过滤器的干扰，有可能会出现异常，提示ContentType正在被使用中，不能修改
                context.Response.ContentType = MediaTypeNames.Application.Json;
                var response = context.Response;
                var errorResponse = new ErrorResponse
                {
                    Success = false
                };

                //判断类型
                switch (exception)
                {
                    case ApplicationException ex:
                        {
                            break;
                        }
                    case NotImplementedException ex:
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.Message = exception.Message;
                        break;

                }

                logger.LogError(exception.Message);
                var res = JsonConvert.SerializeObject(errorResponse);
                await context.Response.WriteAsync(res);
            }
        }
    }


    public class ErrorResponse
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; }
    }
}
