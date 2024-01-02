using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace My.MvcApplicationModel.Study.Conventions
{
    public class ActionDescriptionAttribute : Attribute, IActionModelConvention
    {
        private readonly string _desc;
        public ActionDescriptionAttribute(string description){
            _desc = description;
        }
        public void Apply(ActionModel action)
        {
            action.Properties["Desc3"] = _desc;
        }
    }
}
