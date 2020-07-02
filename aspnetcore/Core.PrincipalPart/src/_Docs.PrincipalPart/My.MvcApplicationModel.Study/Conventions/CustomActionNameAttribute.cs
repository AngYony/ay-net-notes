using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace My.MvcApplicationModel.Study.Conventions
{
    public class CustomActionNameAttribute : Attribute, IActionModelConvention
    {
        private readonly string _actionName;

        public CustomActionNameAttribute(string actionName)
        {
            _actionName = actionName;
        }

        public void Apply(ActionModel action)
        {
            action.ActionName = _actionName;
        }
    }
}