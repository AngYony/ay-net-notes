using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docs.DependencyInjection.Sample.interfaces;
using Docs.DependencyInjection.Sample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Docs.DependencyInjection.Sample.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMyDependency myDependency;

        public IndexModel(IMyDependency _myDependency){
            myDependency = _myDependency;
        }

        public async Task OnGetAsync()
        {
            await myDependency.WriteMessage("在IndexModel.OnGetAsync中创建消息");
        }
    }
}