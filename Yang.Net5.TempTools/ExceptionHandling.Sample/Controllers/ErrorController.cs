using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExceptionHandling.Sample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
         
        [Route("/error")]
        [HttpPost]
        public IActionResult Error()
        {
            // 创建ProblemDetails响应
            return Problem();
        }

       
        [Route("/error-local-development")]
        [HttpPost]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "非开发环境");
            }
            //从中间件中获取实现了该异常的方法
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message
            );
        }


    }
}
