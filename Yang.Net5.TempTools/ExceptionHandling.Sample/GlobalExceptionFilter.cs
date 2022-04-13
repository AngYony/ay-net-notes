using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExceptionHandling.Sample
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var ex= context.Exception;
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
}
