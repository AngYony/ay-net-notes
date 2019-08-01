using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCookieAuthSample2.ViewModels
{
    public class ProcessConsentResult
    {
        public string RedirectUrl{ get; set; }
        public bool IsRedirect => !string.IsNullOrEmpty(RedirectUrl);

        public string ValidationError{ get; set; }


        public ConsentViewModel ViewModel{ get; set; }
    }
}
