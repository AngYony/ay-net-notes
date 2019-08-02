using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCookieAuthSample3.ViewModels
{
    public class InputConsentViewModel
    {
        public string Button{ get; set; }
        public IEnumerable<string> ScopesConsented { get; set; } 

        public bool RemeberConsent{ get; set; }
        public string ReturnUrl{ get; set; }
    }
}
