using Docs.Middleware.Sample.Data;
using Docs.Middleware.Sample.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Middleware.Sample.Pro
{
    public class FactoryActivatedMiddleware : IMiddleware
    {
        private readonly AppDbContext _db;
        public FactoryActivatedMiddleware(AppDbContext db)
        {
            _db = db;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var keyValue = context.Request.Query["key"];
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                _db.Add(new Student("工厂方式：" + keyValue));
                await _db.SaveChangesAsync();
            }
            await next(context);
        }
    }
}
