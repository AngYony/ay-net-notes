using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.TagHelpers.Study.CusTagHelpers
{
    [HtmlTargetElement("email",TagStructure =TagStructure.WithoutEndTag)]
    public class EmailTagHelper:TagHelper
    {
        private const string EmailDomain = "163.com";
        public string MailTo { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            var address = $"{MailTo}@{EmailDomain}";
            output.Attributes.SetAttribute("href", "mailto:" + address);
            output.Content.SetContent(address);
            output.TagMode = TagMode.StartTagAndEndTag;
        }
    }
}
