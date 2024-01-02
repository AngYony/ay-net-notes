using Microsoft.AspNetCore.Razor.TagHelpers;
using My.TagHelpers.Study.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.TagHelpers.Study.CusTagHelpers
{
    //[HtmlTargetElement("myModel")]
    public class MyModelTagHelper:TagHelper
    {
        public WebsiteContext MyInfo { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "section";
            output.Content.SetHtmlContent($@"
<ul>
    <li>Version：{MyInfo.Version}</li>
    <li>Approved：{MyInfo.Approved}</li>
    <li>CopyrightYear：{MyInfo.CopyrightYear}</li>
</ul>");
            output.TagMode = TagMode.StartTagAndEndTag;

        }
    }
}
