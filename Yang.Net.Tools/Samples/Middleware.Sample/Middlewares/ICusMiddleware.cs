using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Sample.Middlewares
{
    public interface ICusMiddleware
    {
        Task InvokeAsync(HttpContext context);
    }
}
