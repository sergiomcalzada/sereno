using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Configuration;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sereno.STS.UI.Controllers;
using Sereno.STS.UI.Pages.Shared;


namespace Sereno.STS.UI.Pages.Device
{
    public class UserCodeConfirmationModel : PageModel
    {
        private readonly IEventService _events;
        private readonly IOptions<IdentityServerOptions> _options;
        private readonly IDeviceFlowInteractionService _interaction;
        private readonly ILogger<UserCodeConfirmationModel> _logger;


        public UserCodeConfirmationModel(ILogger<UserCodeConfirmationModel> logger,
            IDeviceFlowInteractionService interaction, IEventService events,
            IOptions<IdentityServerOptions> options)
        {
            this._logger = logger;
            this._interaction = interaction;
            this._events = events;
            this._options = options;
        }

        public string ClientName { get; set; }
        public string ClientUrl { get; set; }
        public string ClientLogoUrl { get; set; }
        public bool AllowRememberConsent { get; set; }
        public IEnumerable<ScopeModel> IdentityScopes { get; set; }
        public IEnumerable<ScopeModel> ApiScopes { get; set; }


        [BindProperty] public InputModel Input { get; set; } = new InputModel();
        [TempData] public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userCode)
        {
            if (string.IsNullOrWhiteSpace(userCode))
            {
                await this.BuildViewModelAsync(userCode);
                return this.Page();
            }

            return this.RedirectToPage("/Error");
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost()
        {

            var result = await this.ProcessConsent();
            if (result.HasValidationError)
            {
                return this.RedirectToPage("/Error");
            };

            return this.RedirectToPage("Success");
        }


        private async Task<ProcessConsentResult> ProcessConsent()
        {
            var model = this.Input;
            var result = new ProcessConsentResult();

            var request = await this._interaction.GetAuthorizationContextAsync(model.UserCode);
            if (request == null) return result;

            ConsentResponse grantedConsent = null;

            // user clicked 'no' - send back the standard 'access_denied' response
            if (model.Button == "no")
            {
                grantedConsent = new ConsentResponse { Error = AuthorizationError.AccessDenied };

                // emit event
                await this._events.RaiseAsync(new ConsentDeniedEvent(this.User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.RawScopeValues));
            }
            // user clicked 'yes' - validate the data
            else if (model.Button == "yes")
            {
                // if the user consented to some scope, build the response model
                if (model.ScopesConsented != null && model.ScopesConsented.Any())
                {
                    var scopes = model.ScopesConsented;
                    if (ConsentOptions.EnableOfflineAccess == false)
                    {
                        scopes = scopes.Where(x => x != IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess);
                    }

                    grantedConsent = new ConsentResponse
                    {
                        RememberConsent = model.RememberConsent,
                        ScopesValuesConsented = scopes.ToArray(),
                        Description = model.Description
                    };

                    // emit event
                    await this._events.RaiseAsync(new ConsentGrantedEvent(this.User.GetSubjectId(), request.Client.ClientId, request.ValidatedResources.RawScopeValues, grantedConsent.ScopesValuesConsented, grantedConsent.RememberConsent));
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
                await this._interaction.HandleRequestAsync(model.UserCode, grantedConsent);

                // indicate that's it ok to redirect back to authorization endpoint
                result.RedirectUri = model.ReturnUrl;
                result.Client = request.Client;
            }
            else
            {
                // we need to redisplay the consent UI
                result.ShowView = await this.BuildViewModelAsync(model.UserCode);
            }

            return result;
        }

        private async Task<bool> BuildViewModelAsync(string userCode)
        {
            var request = await this._interaction.GetAuthorizationContextAsync(userCode);
            if (request != null)
            {
                this.CreateConsentViewModel(userCode, request);
                return true;
            }
            this._logger.LogError("No consent request matching user code: {0}", userCode);
            return false;
        }

        private void CreateConsentViewModel(string userCode, DeviceFlowAuthorizationRequest request)
        {
            var model = this.Input;
            var check = model == null;
            var scopesConsented = model?.ScopesConsented.ToArray() ?? Array.Empty<string>();

            this.Input = new InputModel
            {
                UserCode = userCode,
                Description = model?.Description,

                RememberConsent = model?.RememberConsent ?? true,
                ScopesConsented = model?.ScopesConsented ?? Enumerable.Empty<string>(),

            };

            this.ClientName = request.Client.ClientName ?? request.Client.ClientId;
            this.ClientUrl = request.Client.ClientUri;
            this.ClientLogoUrl = request.Client.LogoUri;
            this.AllowRememberConsent = request.Client.AllowRememberConsent;
            this.IdentityScopes = ScopeModel.GetIdentityResourcesFromRequest(request.ValidatedResources, scopesConsented, check);

            this.ApiScopes = ScopeModel.GetApiScopeModelsFromRequest(request.ValidatedResources, scopesConsented, check);

        }


        public class InputModel
        {
            public string UserCode { get; set; }
            public string Button { get; set; }
            public IEnumerable<string> ScopesConsented { get; set; }
            public bool RememberConsent { get; set; }
            public string Description { get; set; }
            public string ReturnUrl { get; set; }
        }

        public class ProcessConsentResult
        {
            public string RedirectUri { get; set; }
            public Client Client { get; set; }

            public bool ShowView { get; set; }

            public bool HasValidationError => this.ValidationError != null;
            public string ValidationError { get; set; }
        }


    }
}