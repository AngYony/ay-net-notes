using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Docs.Options.Sample.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace Docs.Options.Sample.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(
            IOptionsMonitor<MyOptions> optionsAccessor,
            IOptionsMonitor<MyOptionsWithDelegateConfig> optionsAccessorWithDelegateConfig,
            IOptionsMonitor<MySubOptions> subOptionsAccessor,

            IOptionsSnapshot<MyOptions> snapshotOptionsAccessor,
            IOptionsSnapshot<MyOptions> namedOptionsAccessor,
            IOptionsMonitor<MyOptions> addOptionsAccessor
        )
        {


            _options  = optionsAccessor.CurrentValue;
            _optionsWithDelegateConfig = optionsAccessorWithDelegateConfig.CurrentValue;
            _subOptions =subOptionsAccessor.CurrentValue;
            _snapshotOptions = snapshotOptionsAccessor.Value;

            _named_options_1= namedOptionsAccessor.Get("named_options_1");
            _named_options_2 = namedOptionsAccessor.Get("named_options_2");

            addOptions = addOptionsAccessor.Get("wyOptions");
        }


        public MyOptions _options { get; set; }
        

        public MyOptionsWithDelegateConfig _optionsWithDelegateConfig { get; set; }

        public MySubOptions _subOptions{ get; set; }

        public MyOptions _snapshotOptions{ get; set; }

        public MyOptions _named_options_1 { get; set; }
        public MyOptions _named_options_2 { get; set; }

        public MyOptions addOptions { get; set; }
        public void OnGet()
        {

        }
    }
}