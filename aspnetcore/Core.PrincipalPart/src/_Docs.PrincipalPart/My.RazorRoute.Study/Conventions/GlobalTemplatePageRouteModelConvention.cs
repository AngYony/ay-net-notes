using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My.RazorRoute.Study.Conventions
{
    public class GlobalTemplatePageRouteModelConvention : IPageRouteModelConvention
    {
        ILogger _logger;
        public GlobalTemplatePageRouteModelConvention(ILogger logger)
        {
            _logger = logger;
        }
        public void Apply(PageRouteModel model)
        {
            StringBuilder log = new StringBuilder();
            log.AppendLine("==================================");
            log.AppendLine($"Count：{model.Selectors.Count}  ViewEnginePath:{model.ViewEnginePath}    RelativePath:{model.RelativePath}");

            var selectorCount = model.Selectors.Count;
            for (var i = 0; i < selectorCount; i++)
            {
                var selector = model.Selectors[i];
                log.AppendLine("未添加前");
                log.AppendLine($"Order：{selector.AttributeRouteModel.Order} ， Template：{selector.AttributeRouteModel.Template}");

                //在现有的基础上添加新的路由模板
                model.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel
                    {
                        Order = 1,
                        //有当前的模板和自定义模板合并为一个新的模板
                        Template = AttributeRouteModel.CombineTemplates(
                            //获取当前的模板
                            selector.AttributeRouteModel.Template,
                            "{globalTemplate?}"),
                    }

                });

                log.AppendLine($"添加完之后，Count:{model.Selectors.Count}");
                foreach (var s in model.Selectors)
                {
                    log.AppendLine($"Order：{s.AttributeRouteModel.Order} ， Template：{s.AttributeRouteModel.Template}");
                }

                _logger.LogDebug(log.ToString());
            }
        }
    }
}
