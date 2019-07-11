using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Middleware.Sample.Dev
{
    public static class DevMiddlewareExtensions
    {
        public static IApplicationBuilder UseOctOceanMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DevMiddleware>();
        }
    }
}
