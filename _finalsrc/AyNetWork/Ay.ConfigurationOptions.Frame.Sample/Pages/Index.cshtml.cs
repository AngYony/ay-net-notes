using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ay.ConfigurationOptions.Frame.Sample.OptionsModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Ay.ConfigurationOptions.Frame.Sample.Pages
{
    public class IndexModel : PageModel
    {
        public readonly AyOption ayOption;
        public readonly AyOption ayOption2;

        public IndexModel(IOptionsMonitor<AyOption> optionsAccessor, IOptionsSnapshot<AyOption> snapshotOptionsAccessor)
        {
            ayOption = snapshotOptionsAccessor.Value;
            ayOption2= optionsAccessor.CurrentValue;
        }
        public void OnGet()
        {

        }
    }
}