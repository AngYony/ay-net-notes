using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCookieAuthSample2.ViewModels
{
    public class RegisterViewModel
    {
        public string Email{ get; set; }
        public string Password{ get; set; }
        public string ConfirmedPassword{ get; set; }
    }
}
