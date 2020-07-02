using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace My.MvcApplicationModel.Study.Conventions
{
    public class ControllerDescriptionAttribute : Attribute, IControllerModelConvention
    {
        private readonly string _desc;
        public ControllerDescriptionAttribute(string description)
        {
            _desc = description;
        }
        public void Apply(ControllerModel controller)
        {
            controller.Properties["Desc"] = _desc;
        }
    }
}
