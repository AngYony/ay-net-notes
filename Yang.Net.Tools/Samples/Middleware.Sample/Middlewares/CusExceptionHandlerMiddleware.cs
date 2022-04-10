using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;
using System.Text.Json.Serialization;

namespace Middleware.Sample.Middlewares
{
    public class CusExceptionHandlerMiddleware : ICusMiddleware
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
                await NewMethod(context, exception);
            }

            async Task NewMethod(HttpContext context, Exception exception)
            {
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
