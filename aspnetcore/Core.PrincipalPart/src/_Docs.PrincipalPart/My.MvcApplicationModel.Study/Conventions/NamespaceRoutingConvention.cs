using System.Linq;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace My.MvcApplicationModel.Study.Conventions
{
    public class NamespaceRoutingConvention : IApplicationModelConvention
    {
        public void Apply(ApplicationModel application)
        {
            foreach (var controller in application.Controllers)
            {
                var hasAttributeRouteModels = controller.Selectors.Any(a => a.AttributeRouteModel != null);
                if (!hasAttributeRouteModels
                && controller.ControllerName.Contains("Namespace"))
                {
                    controller.Selectors[0].AttributeRouteModel = new AttributeRouteModel()
                    {
                        Template = controller.ControllerType.Namespace.Replace('.', '/') 
                        + "/[controller]/[action]/{id?}"
                    };
                }
            }
        }
    }
}