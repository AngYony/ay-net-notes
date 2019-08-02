using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using MvcCookieAuthSample3.Services;
using MvcCookieAuthSample3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCookieAuthSample3.Controllers
{
 
    public class ConsentController:Controller
    {
        private readonly ConsentService _consentService;
       
        public ConsentController(ConsentService consentService)
        {
            _consentService = consentService;
        }
     


        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var model = await _consentService. BuildConsentViewModelAsync(returnUrl);

            if(model==null)
            {

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(InputConsentViewModel viewModel)
        {
            var result=await _consentService.PorcessConsent(viewModel);

           if(result.IsRedirect)
           {
                return Redirect(result.RedirectUrl);
           }

           if(!string.IsNullOrEmpty(result.ValidationError))
           {
                ModelState.AddModelError("", result.ValidationError);
           }
             return View(result.ViewModel);
           

        } 
    }
}
