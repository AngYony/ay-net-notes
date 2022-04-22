using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace WebApiStartup.Sample
{
    internal class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var ex = context.Exception;
            ex.GetBaseException();
            switch (ex)
            {
                case MyException mye:
                    break;
                default:
                    break;
            }
            return Task.CompletedTask;//throw new NotImplementedException();
        }
    }

    public class MyException : Exception
    {

        public MyException(string? message, Exception innerException) : base(message, innerException)
        {

        }
    }
}