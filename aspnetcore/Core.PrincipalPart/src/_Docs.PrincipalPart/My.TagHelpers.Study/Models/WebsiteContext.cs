using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace My.TagHelpers.Study.Models
{
    public class WebsiteContext
    {
        public Version Version { get; set; }
        public int CopyrightYear { get; set; }
        public bool Approved { get; set; }
        public int TagsToShow { get; set; }
    }
}
