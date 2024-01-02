using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace My.Filters.Study.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            var r = Request;
            var s = Response;
            var h = HttpContext;
            _logger = logger;
        }

        public string Message { get; set; }


        public void OnGet()
        {
            _logger.LogInformation("Index Get ................");
        }


        public override void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            base.OnPageHandlerSelected(context);
            _logger.LogDebug("IndexModel/OnPageHandlerSelected...........");
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            Message = "Message set in handler executing";
            base.OnPageHandlerExecuting(context);
            _logger.LogDebug("IndexModel/OnPageHandlerExecuting...........");
        }

        public override void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
            base.OnPageHandlerExecuted(context);
            _logger.LogDebug("IndexModel/OnPageHandlerExecuted...........");
        }
        
    }
}