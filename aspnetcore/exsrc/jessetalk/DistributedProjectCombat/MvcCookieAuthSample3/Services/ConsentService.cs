using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using MvcCookieAuthSample3.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCookieAuthSample3.Services
{
    public class ConsentService
    {
        private readonly IClientStore _clientStore;
        private readonly IResourceStore _resourceStore;
        private readonly IIdentityServerInteractionService _identityServerInteractionService;
        public ConsentService(
        IClientStore clientStore,
        IResourceStore resourceStore,
        IIdentityServerInteractionService identityServerInteractionService)
        {
            _clientStore = clientStore;
            _resourceStore = resourceStore;
            _identityServerInteractionService = identityServerInteractionService;

        }


        


        private ConsentViewModel CreateConsentViewModel(AuthorizationRequest request, 
        Client client, Resources resources,InputConsentViewModel model)
        {
            var selectedScopes = model?.ScopesConsented ?? Enumerable.Empty<string>();


            var vm = new ConsentViewModel();

            vm.ClientName = client.ClientName;
            vm.ClientLogoUrl = client.LogoUri;
            vm.ClientUrl = client.ClientUri;
            vm.RemeberConsent = model?.RemeberConsent ?? true;

            vm.IdentityScopes = resources.IdentityResources.Select(_identityResource => {

                
                return new ScopeViewModel
                {

                    Name = _identityResource.Name,
                    DisplayName = _identityResource.DisplayName,
                    Description = _identityResource.Description,
                    Required = _identityResource.Required,
                    Checked = selectedScopes.Contains(_identityResource.Name) || _identityResource.Required||model==null,
                    Emphasize = _identityResource.Emphasize
                };

            });

            vm.ResourceScopes = resources.ApiResources.SelectMany(i => i.Scopes).Select(_scope => {
                return new ScopeViewModel
                {

                    Name = _scope.Name,
                    DisplayName = _scope.DisplayName,
                    Description = _scope.Description,
                    Required = _scope.Required,
                    Checked = selectedScopes.Contains(_scope.Name) || _scope.Required||model==null,
                    Emphasize = _scope.Emphasize
                };
            });

            return vm;
        }



        public async Task<ConsentViewModel> BuildConsentViewModelAsync(string returnUrl,InputConsentViewModel model=null)
        {
            var request = await _identityServerInteractionService.GetAuthorizationContextAsync(returnUrl);
            if (request == null)
                return null;


            var client = await _clientStore.FindEnabledClientByIdAsync(request.ClientId);
            var resources = await _resourceStore.FindEnabledResourcesByScopeAsync(request.ScopesRequested);

            var vm = CreateConsentViewModel(request, client, resources,model);
            vm.ReturnUrl = returnUrl;
            return vm;
        }


        public async Task<ProcessConsentResult> PorcessConsent(InputConsentViewModel model)
        {
            var result = new ProcessConsentResult();

            ConsentResponse consentResponse = null;

            if (model.Button == "no")
            {
                consentResponse = ConsentResponse.Denied;
            }
            else if (model.Button == "yes")
            {
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    consentResponse = new ConsentResponse
                    {
                        RememberConsent = model.RemeberConsent,
                        ScopesConsented = model.ScopesConsented
                    };
                }

                result.ValidationError = "请至少选中一个允许权限";
            }





            if (consentResponse != null)
            {
                var request = await _identityServerInteractionService.GetAuthorizationContextAsync(model.ReturnUrl);
                await _identityServerInteractionService.GrantConsentAsync(request, consentResponse);
                result.RedirectUrl = model.ReturnUrl;
            }
            else{
                result.ViewModel = await BuildConsentViewModelAsync(model.ReturnUrl);
            }

            return result;
        }
    }
}
