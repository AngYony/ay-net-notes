using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace My.MvcApplicationModel.Study.Controllers
{
    public class NamespaceRoutingController : Controller
    {
        //访问路由：/my/mvcapplicationmodel/study/controllers/NamespaceRouting/Index
        public string Index()
        {
            return "Namespace";
        }
    }
}