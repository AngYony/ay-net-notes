using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.TagHelpers.Study.CusTagHelpers
{
    [HtmlTargetElement(Attributes =nameof(Where))]
    public class WhereTagHelper:TagHelper
    {
        public bool Where { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!Where)
            {
                output.SuppressOutput();
            }
        }
    }
}
