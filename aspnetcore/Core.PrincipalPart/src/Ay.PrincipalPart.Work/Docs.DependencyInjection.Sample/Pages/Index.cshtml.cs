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
        //private readonly IMyDependency myDependency;

        //public IndexModel(IMyDependency _myDependency){
        //    myDependency = _myDependency;
        //}


        public readonly IOperationTransient OperationTransient;
        public readonly IOperationScoped OperationScoped;
        public readonly IOperationSingleton OperationSingleton;
        public readonly IOperationSingletonInstance OperationSingletonInstance;

        public OperationService OperationService { get; }


        public IndexModel(
            IOperationTransient operationTransient,
            IOperationScoped operationScoped,
            IOperationSingleton operationSingleton,
            IOperationSingletonInstance operationSingletonInstance,

            OperationService operationService
        )
        {
            this.OperationTransient = operationTransient;
            this.OperationScoped = operationScoped;
            this.OperationSingleton = operationSingleton;
            this.OperationSingletonInstance = operationSingletonInstance;

            this.OperationService = operationService;
        }


        [BindProperty]
        public Student Student{ get; set; }


        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }





        //public async Task OnGetAsync()
        //{
        //    await myDependency.WriteMessage("在IndexModel.OnGetAsync中创建消息");
        //}
    }
}