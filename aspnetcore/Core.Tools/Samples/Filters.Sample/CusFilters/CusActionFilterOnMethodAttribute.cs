using Filters.Sample.Services;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters.Sample.CusFilters
{
    public class CusActionFilterOnMethodAttribute : Attribute, IActionFilter
    {
        //IUser通过TypeFilter注入
        public CusActionFilterOnMethodAttribute(IUser user)
        {
            User = user;
        }

        public IUser User { get; }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("在方法上执行：OnActionExecuted");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("在方法上执行：OnActionExecuting");
        }
    }
}
