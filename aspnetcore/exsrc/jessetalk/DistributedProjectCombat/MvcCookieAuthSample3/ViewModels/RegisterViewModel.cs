using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCookieAuthSample3.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName{ get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password{ get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string ConfirmedPassword{ get; set; }
    }
}
