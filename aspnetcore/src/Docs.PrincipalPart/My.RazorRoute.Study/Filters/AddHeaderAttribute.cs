using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.RazorRoute.Study.Filters
{
    public class AddHeaderAttribute: ResultFilterAttribute
    {
        private readonly string _name;
        private readonly string[] _values;

        public AddHeaderAttribute(string name,string [] values)
        {
            _name = name;
            _values = values;
        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add(_name, _values);
            base.OnResultExecuting(context);
        }
    }
}
