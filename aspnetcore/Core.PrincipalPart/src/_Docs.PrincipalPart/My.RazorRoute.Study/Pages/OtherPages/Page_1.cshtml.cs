using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace My.RazorRoute.Study.Pages
{
    public class Page_1 : PageModel
    {
        public string RouteDataGlobalTemplateValue { get; private set; }

        public string RouteDataOtherPagesTemplateValue { get; private set; }

        public void OnGet()
        {
            if(RouteData.Values["globalTemplate"]!=null)
            {
                RouteDataGlobalTemplateValue = $"globalTemplate提供了路由数据：{RouteData.Values["globalTemplate"]}";
            }

            if (RouteData.Values["otherPagesTemplate"] != null)
            {
                RouteDataOtherPagesTemplateValue =
                     $"otherPagesTemplate：{RouteData.Values["otherPagesTemplate"]}"; 
            }

        }
    }
}