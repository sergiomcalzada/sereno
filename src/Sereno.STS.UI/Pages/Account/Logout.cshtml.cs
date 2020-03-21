using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Sereno.Domain.Entity;

namespace Sereno.STS.UI.Pages.Account
{
    [AllowAnonymous]
    public class LogoutModel : PageModel
    {
        private readonly IEventService events;
        private readonly IIdentityServerInteractionService interaction;
        private readonly ILogger<LogoutModel> logger;
        private readonly SignInManager<User> signInManager;

        public LogoutModel(ILogger<LogoutModel> logger,
            IIdentityServerInteractionService interaction,
            IEventService events,
            SignInManager<User> signInManager)
        {
            this.signInManager = signInManager;
            this.logger = logger;
            this.interaction = interaction;
            this.events = events;
        }

        public bool ShowLogoutPrompt { get; set; }

        public string LogoutId { get; set; }

        public string ExternalAuthenticationScheme { get; set; }

        public async Task<IActionResult> OnGet(string logoutId)
        {
            // build a model so the logout page knows what to display
            await this.BuildLogoutViewModelAsync(logoutId);

            if (this.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await this.OnPost(logoutId);
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPost(string logoutId)
        {
            // build a model so the logged out page knows what to display
            await this.BuildLoggedOutViewModelAsync();

            if (this.User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                await this.signInManager.SignOutAsync();

                // raise the logout event
                await this.events.RaiseAsync(new UserLogoutSuccessEvent(this.User.GetSubjectId(), this.User.GetDisplayName()));
            }

            // check if we need to trigger sign-out at an upstream identity provider
            if (!this.ExternalAuthenticationScheme.IsNullOrEmpty())
            {
                // build a return URL so the upstream provider will redirect back
                // to us after the user has logged out. this allows us to then
                // complete our single sign-out processing.
                var url = this.Url.Page("Logout", new { logoutId });

                // this triggers a redirect to the external provider for sign-out
                return this.SignOut(new AuthenticationProperties { RedirectUri = url }, this.ExternalAuthenticationScheme);
            }

            return this.RedirectToPage("LoggedOut", new { logoutId });
        }

        private async Task BuildLogoutViewModelAsync(string logoutId)
        {
            this.LogoutId = logoutId;
            this.ShowLogoutPrompt = true;

            if (this.User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                this.ShowLogoutPrompt = false;
            }

            var context = await this.interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                this.ShowLogoutPrompt = false;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
        }

        private async Task BuildLoggedOutViewModelAsync()
        {
            if (this.User?.Identity.IsAuthenticated == true)
            {
                var idp = this.User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignOut = await this.HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignOut)
                    {
                        if (this.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we SignOut and redirect away to the external IdP for SignOut
                            this.LogoutId = await this.interaction.CreateLogoutContextAsync();
                        }

                        this.ExternalAuthenticationScheme = idp;
                    }
                }
            }
        }
    }
}