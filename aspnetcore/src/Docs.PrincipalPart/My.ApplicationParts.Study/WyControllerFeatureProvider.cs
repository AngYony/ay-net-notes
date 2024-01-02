using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace My.ApplicationParts.Study
{
    public class WyControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(
            IEnumerable<Microsoft.AspNetCore.Mvc.ApplicationParts.ApplicationPart> parts,
            ControllerFeature feature)
        {
            foreach (var entityType in EntityTypes.MyTypes)
            {
                var typeName = entityType.Name + "Controller";
                if (!feature.Controllers.Any(t => t.Name == typeName))
                {
                    var s = typeof(WyController<>); //可以添加断点跟踪该值

                    var controllerType = typeof(WyController<>)
                    .MakeGenericType(entityType.AsType()).GetTypeInfo();

                    feature.Controllers.Add(controllerType);
                }
            }
        }
    }
}