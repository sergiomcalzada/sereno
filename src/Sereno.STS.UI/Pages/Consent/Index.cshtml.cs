using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Sereno.STS.UI.Controllers;
using Sereno.STS.UI.Controllers.Consent;
using static System.String;

namespace Sereno.STS.UI.Pages.Consent
{
    public class IndexModel : PageModel
    {
        private readonly IEventService _events;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly ILogger<IndexModel> _logger;


        public IndexModel(ILogger<IndexModel> logger, IIdentityServerInteractionService interaction,
            IEventService events)
        {
            this._logger = logger;
            this._interaction = interaction;
            this._events = events;
        }

        public string ClientName { get; set; }
        public string ClientUrl { get; set; }
        public string ClientLogoUrl { get; set; }
        public bool AllowRememberConsent { get; set; }
        public IEnumerable<ScopeModel> IdentityScopes { get; set; }
        public IEnumerable<ScopeModel> ApiScopes { get; set; }


        [BindProperty] public InputModel Input { get; set; } = new InputModel();
        [TempData] public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string returnUrl)
        {
            var canCreate = await this.BuildViewModelAsync(returnUrl);
            if (canCreate)
            {
                return this.Page();
            }

            return this.RedirectToPage("Error");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost()
        {
            var model = this.Input;
            var result = await this.ProcessConsent();

            if (result.IsRedirect)
            {
                var context = await this._interaction.GetAuthorizationContextAsync(model.ReturnUrl);
                if (context?.IsNativeClient() == true)
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.RedirectToPage("Redirect", result.RedirectUri);
                }

                return this.Redirect(result.RedirectUri);
            }

            if (result.HasValidationError)
            {
                this.ModelState.AddModelError(Empty, result.ValidationError);
            }

            if (result.ShowView)
            {
                return this.Page();
            }

            return this.RedirectToPage("Error");
        }

        /*****************************************/
        /* helper APIs for the ConsentController */
        /*****************************************/
        private async Task<ProcessConsentResult> ProcessConsent()
        {
            var model = this.Input;
            var result = new ProcessConsentResult();

            // validate return url is still valid
            var request = await this._interaction.GetAuthorizationContextAsync(model.ReturnUrl);
            if (request == null)
            {
                return result;
            }

            ConsentResponse grantedConsent = null;

            // user clicked 'no' - send back the standard 'access_denied' response
            if (model?.Button == "no")
            {
                grantedConsent = new ConsentResponse {Error = AuthorizationError.AccessDenied};

                // emit event
                await this._events.RaiseAsync(new ConsentDeniedEvent(this.User.GetSubjectId(), request.Client.ClientId,
                    request.ValidatedResources.RawScopeValues));
            }
            // user clicked 'yes' - validate the data
            else if (model?.Button == "yes")
            {
                // if the user consented to some scope, build the response model
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    var scopes = model.ScopesConsented;
                    if (ConsentOptions.EnableOfflineAccess == false)
                    {
                        scopes = scopes.Where(x => x != IdentityServerConstants.StandardScopes.OfflineAccess);
                    }

                    grantedConsent = new ConsentResponse
                    {
                        RememberConsent = model.RememberConsent,
                        ScopesValuesConsented = scopes.ToArray(),
                        Description = model.Description
                    };

                    // emit event
                    await this._events.RaiseAsync(new ConsentGrantedEvent(this.User.GetSubjectId(),
                        request.Client.ClientId, request.ValidatedResources.RawScopeValues,
                        grantedConsent.ScopesValuesConsented, grantedConsent.RememberConsent));
                }
                else
                {
                    result.ValidationError = ConsentOptions.MustChooseOneErrorMessage;
                }
            }
            else
            {
                result.ValidationError = ConsentOptions.InvalidSelectionErrorMessage;
            }

            if (grantedConsent != null)
            {
                // communicate outcome of consent back to identityserver
                await this._interaction.GrantConsentAsync(request, grantedConsent);

                // indicate that's it ok to redirect back to authorization endpoint
                result.RedirectUri = model.ReturnUrl;
                result.Client = request.Client;
            }
            else
            {
                // we need to redisplay the consent UI
                result.ShowView = await this.BuildViewModelAsync(model.ReturnUrl);
            }

            return result;
        }

        private async Task<bool> BuildViewModelAsync(string returnUrl)
        {
            var request = await this._interaction.GetAuthorizationContextAsync(returnUrl);
            if (request != null)
            {
                this.CreateConsentViewModel(returnUrl, request);
                return true;
            }

            this._logger.LogError("No consent request matching request: {0}", returnUrl);

            return false;
        }

        private void CreateConsentViewModel(string returnUrl, AuthorizationRequest request)
        {
            var model = this.Input;
            var vm = new InputModel
            {
                RememberConsent = model?.RememberConsent ?? true,
                ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>(),
                Description = model?.Description,

                ReturnUrl = returnUrl
            };

            this.ClientName = request.Client.ClientName ?? request.Client.ClientId;
            this.ClientUrl = request.Client.ClientUri;
            this.ClientLogoUrl = request.Client.LogoUri;
            this.AllowRememberConsent = request.Client.AllowRememberConsent;
            this.IdentityScopes = request.ValidatedResources.Resources.IdentityResources.Select(x =>
                this.CreateScopeViewModel(x, vm.ScopesConsented.Contains(x.Name) || model == null)).ToArray();

            var apiScopes = new List<ScopeModel>();
            foreach (var parsedScope in request.ValidatedResources.ParsedScopes)
            {
                var apiScope = request.ValidatedResources.Resources.FindApiScope(parsedScope.ParsedName);
                if (apiScope != null)
                {
                    var scopeVm = this.CreateScopeViewModel(parsedScope, apiScope,
                        vm.ScopesConsented.Contains(parsedScope.RawValue) || model == null);
                    apiScopes.Add(scopeVm);
                }
            }

            if (ConsentOptions.EnableOfflineAccess && request.ValidatedResources.Resources.OfflineAccess)
            {
                apiScopes.Add(this.GetOfflineAccessScope(
                    vm.ScopesConsented.Contains(IdentityServerConstants.StandardScopes.OfflineAccess) ||
                    model == null));
            }

            this.ApiScopes = apiScopes;

            this.Input = vm;
        }

        private ScopeModel CreateScopeViewModel(IdentityResource identity, bool check)
        {
            return new ScopeModel
            {
                Value = identity.Name,
                DisplayName = identity.DisplayName ?? identity.Name,
                Description = identity.Description,
                Emphasize = identity.Emphasize,
                Required = identity.Required,
                Checked = check || identity.Required
            };
        }

        public ScopeModel CreateScopeViewModel(ParsedScopeValue parsedScopeValue, ApiScope apiScope, bool check)
        {
            var displayName = apiScope.DisplayName ?? apiScope.Name;
            if (!IsNullOrWhiteSpace(parsedScopeValue.ParsedParameter))
            {
                displayName += ":" + parsedScopeValue.ParsedParameter;
            }

            return new ScopeModel
            {
                Value = parsedScopeValue.RawValue,
                DisplayName = displayName,
                Description = apiScope.Description,
                Emphasize = apiScope.Emphasize,
                Required = apiScope.Required,
                Checked = check || apiScope.Required
            };
        }

        private ScopeModel GetOfflineAccessScope(bool check)
        {
            return new ScopeModel
            {
                Value = IdentityServerConstants.StandardScopes.OfflineAccess,
                DisplayName = ConsentOptions.OfflineAccessDisplayName,
                Description = ConsentOptions.OfflineAccessDescription,
                Emphasize = true,
                Checked = check
            };
        }

        public class InputModel
        {
            public string Button { get; set; }
            public IEnumerable<string> ScopesConsented { get; set; }
            public bool RememberConsent { get; set; }
            public string ReturnUrl { get; set; }
            public string Description { get; set; }
        }

        public class ScopeModel
        {
            public string Value { get; set; }
            public string DisplayName { get; set; }
            public string Description { get; set; }
            public bool Emphasize { get; set; }
            public bool Required { get; set; }
            public bool Checked { get; set; }
        }

        public class ProcessConsentResult
        {
            public bool IsRedirect => this.RedirectUri != null;
            public string RedirectUri { get; set; }
            public Client Client { get; set; }

            public bool ShowView { get; set; }

            public bool HasValidationError => this.ValidationError != null;
            public string ValidationError { get; set; }
        }
    }
}