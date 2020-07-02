using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.MVC.Study
{
    public class MyCusRouteAttribute : Attribute, IRouteTemplateProvider
    {
        //自定义路由模板
        public string Template => "smallz/[controller]";

        //路由顺序
        public int? Order{get;set;}

        //路由名称
        public string Name { get; set; }
    }
}
