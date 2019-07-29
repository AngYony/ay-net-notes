using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCookieAuthSample2.ViewModels
{
    public class ConsentViewModel
    {
        public string ClientId{ get; set; }
        public string ClientName{ get; set; }

        public string ClientUrl{ get; set; }
        public string ClientLogoUrl{ get; set; }
        public bool AllowRememberConsent{ get; set; }

        public IEnumerable<ScopeViewModel> IdentityScopes{ get; set; }
        public IEnumerable<ScopeViewModel> ResourceScopes{ get; set; }
    }
}
