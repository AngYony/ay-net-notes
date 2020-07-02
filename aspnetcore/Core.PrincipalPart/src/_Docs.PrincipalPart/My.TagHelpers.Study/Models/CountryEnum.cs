using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace My.TagHelpers.Study.Models
{
    public enum CountryEnum
    {
        [Display(Name ="th1")]
        One,
        [Display(Name = "th2")]
        Two,
        Three
    }
}
