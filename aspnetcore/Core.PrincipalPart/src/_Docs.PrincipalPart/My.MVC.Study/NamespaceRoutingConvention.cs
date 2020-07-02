using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Linq;
using System.Text;

namespace My.MVC.Study
{
    public class NamespaceRoutingConvention : IControllerModelConvention
    {
        private readonly string _baseNamespace;

        public NamespaceRoutingConvention(string baseNamespace)
        {
            _baseNamespace = baseNamespace;
        }

        public void Apply(ControllerModel controller)
        {
            var hasRouteAttributes = controller.Selectors.Any(selector => selector.AttributeRouteModel != null);

            if (hasRouteAttributes)
            {
                return;
            }

            var namespc = controller.ControllerType.Namespace;
            if (namespc == null)
            {
                return;
            }
            var template = new StringBuilder();
            template.Append(namespc, _baseNamespace.Length + 1, namespc.Length - _baseNamespace.Length - 1);
            template.Replace('.', '/');
            template.Append("/[controller]");

            foreach (var selector in controller.Selectors)
            {
                selector.AttributeRouteModel = new AttributeRouteModel()
                {
                    Template = template.ToString()
                };
            }
        }
    }
}