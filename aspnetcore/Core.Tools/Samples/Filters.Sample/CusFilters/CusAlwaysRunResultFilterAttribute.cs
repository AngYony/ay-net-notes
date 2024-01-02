using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filters.Sample.CusFilters
{
    //自定义始终会处理Result结果的过滤器
    public class CusAlwaysRunResultFilterAttribute : Attribute, IAlwaysRunResultFilter
    {
        public void OnResultExecuted(ResultExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if(context.Result is StatusCodeResult statusCodeResult 
            && statusCodeResult.StatusCode==StatusCodes.Status415UnsupportedMediaType)
            {
                context.Result = new ObjectResult("Unprocessable")
                {
                    StatusCode = StatusCodes.Status422UnprocessableEntity
                };
            }
        }

    }
}
