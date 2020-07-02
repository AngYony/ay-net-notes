using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using My.MvcApplicationModel.Study.Conventions;

namespace My.MvcApplicationModel.Study.Controllers
{
    [ControllerDescription("My Desc2")]
    public class HomeController : Controller
    {
        [ActionDescription("My Desc3")]
        public string Index()
        {

            return "Desc:" + ControllerContext.ActionDescriptor.Properties["Desc"] +
            "Desc2:" + ControllerContext.ActionDescriptor.Properties["Desc"] +
            "Desc3:" + ControllerContext.ActionDescriptor.Properties["Desc3"];
        }

        [CustomActionName("NewAc")]
        public string SomeName(){
            return ControllerContext.ActionDescriptor.ActionName;
        }

        public string GetById([MustBeInRouteParameterModelConvention]int id)
        {
            return $"Bound to id: {id}";
        }
    }
}