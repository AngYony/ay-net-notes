using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using MvcCookieAuthSample2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCookieAuthSample2.Controllers
{
 
    public class ConsentController:Controller
    {
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        public ConsentController(
        IClientStore clientStore,
        IResourceStore resourceStore,
        IIdentityServerInteractionService identityServerInteractionService     ){
            _clientStore = clientStore;
            _resourceStore = resourceStore;
            _identityServerInteractionService = identityServerInteractionService;

        }

        private async Task<ConsentViewModel> BuildConsentViewModelAsync(string returnUrl){
            var request = await _identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            if (request == null)
                return null;


            var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
            var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);

            var vm= CreateConsentViewModel(request, client, resources);
            vm.ReturnUrl = returnUrl;
            return vm;
        }


        private ConsentViewModel CreateConsentViewModel(AuthorizationRequest request,Client client, Resources resources){
            var vm = new ConsentViewModel();

            vm.ClientName = client.ClientName;
            vm.ClientLogoUrl = client.LogoUri;
            vm.ClientUrl = client.ClientUri;
            vm.RemeberConsent = client.AllowRememberConsent;

            vm.IdentityScopes = resources.IdentityResources.Select(_identityResource => {
                return new ScopeViewModel
                {

                    Name = _identityResource.Name,
                    DisplayName = _identityResource.DisplayName,
                    Description = _identityResource.Description,
                    Required = _identityResource.Required,
                    Checked = _identityResource.Required,
                    Emphasize = _identityResource.Emphasize
                };

            });

            vm.ResourceScopes = resources.ApiResources.SelectMany(i => i.Scopes).Select(_scope => {
                return new ScopeViewModel {

                    Name = _scope.Name,
                    DisplayName = _scope.DisplayName,
                    Description = _scope.Description,
                    Required = _scope.Required,
                    Checked = _scope.Required,
                    Emphasize = _scope.Emphasize
                };
            });

            return vm;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string returnUrl)
        {
            var model = await BuildConsentViewModelAsync(returnUrl);

            if(model==null)
            {

            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(InputConsentViewModel viewModel)
        {
            ConsentResponse consentResponse = null;

            if(viewModel.Button=="no"){
                consentResponse = ConsentResponse.Denied;
            }
            else if(viewModel.Button=="yes"){
                if(viewModel.ScopesConsented!=null && viewModel.ScopesConsented.Any()){
                    consentResponse = new ConsentResponse
                    {
                        RememberConsent = viewModel.RemeberConsent,
                        ScopesConsented = viewModel.ScopesConsented
                    };
                }
            }
            if(consentResponse!=null)
            {
                var request = await _identityServerInteractionService.GetAuthorizationContextAsync(viewModel.ReturnUrl);
                await _identityServerInteractionService.GrantConsentAsync(request, consentResponse);
                return Redirect(viewModel.ReturnUrl);
            }
            else{
                return View();
            }

        } 
    }
}
