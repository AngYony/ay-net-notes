using Microsoft.AspNetCore.Mvc.ApplicationModels;
using My.RazorRoute.Study.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.RazorRoute.Study.Conventions
{
    public class GlobalHeaderPageApplicationModelConvention : IPageApplicationModelConvention
    {
        public void Apply(PageApplicationModel model)
        {
             
            model.Filters.Add(new AddHeaderAttribute("GlobalHeader", 
                new string[] { "Global Header Value" }));
        }
    }
}
