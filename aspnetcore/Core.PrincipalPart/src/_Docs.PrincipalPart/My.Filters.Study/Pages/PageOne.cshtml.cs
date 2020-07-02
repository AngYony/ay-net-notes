using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using My.Filters.Study.Filters;

namespace My.Filters.Study.Pages
{
    [AddHeader("Author", "smallz")]
    public class PageOneModel : PageModel
    {
        private readonly ILogger logger;

        public PageOneModel(ILogger<PageOneModel> logger)
        {
            this.logger = logger;
        }

        public string Message { get; set; }

        public async Task OnGet()
        {
            Message = "Your PageOne page.";
            logger.LogDebug("PageOne/OnGet");
            await Task.CompletedTask;
        }
    }
}