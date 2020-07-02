using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace My.TagHelpers.Study.Models
{
    public class TagViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name ="Email Address")]
        public string Email{ get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password{ get; set; }

        public AddressViewModel Address{ get; set; }

        public List<string> Colors{ get; set; }

        [MinLength(5)]
        [MaxLength(1024)]
        public string Description{ get; set; }


        public string Country{ get; set; }
        public List<SelectListItem> Countries{ get; set; }

        public CountryEnum EnumCountry { get; set; }
    }

    public class AddressViewModel
    {
        public string AddressLine{ get; set; }
    }
}
