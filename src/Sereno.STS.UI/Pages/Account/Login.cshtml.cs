using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Sereno.Domain.Entity;
using Sereno.STS.UI.Extensions;

namespace Sereno.STS.UI.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly IClientStore clientStore;
        private readonly IEventService events;
        private readonly IIdentityServerInteractionService interaction;
        private readonly ILogger<LoginModel> logger;
        private readonly IAuthenticationSchemeProvider schemeProvider;
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public LoginModel(ILogger<LoginModel> logger,
            IIdentityServerInteractionService interaction,
            IEventService events,
            IAuthenticationSchemeProvider schemeProvider,
            IClientStore clientStore,
            SignInManager<User> signInManager,
            UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.interaction = interaction;
            this.events = events;
            this.schemeProvider = schemeProvider;
            this.clientStore = clientStore;
        }

        [BindProperty] public InputModel Input { get; set; } = new InputModel();

        [TempData] public string ErrorMessage { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public bool EnableLocalLogin { get; set; }

        public bool IsExternalLoginOnly => this.EnableLocalLogin == false && this.ExternalLogins?.Count == 1;

        public string ExternalLoginScheme =>
            this.IsExternalLoginOnly ? this.ExternalLogins?.SingleOrDefault()?.Name : null;


        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(this.ErrorMessage))
            {
                this.ModelState.AddModelError(string.Empty, this.ErrorMessage);
            }

            returnUrl = returnUrl ?? this.Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await this.HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            await this.BuildLoginViewModelAsync(returnUrl);

            if (this.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return this.RedirectToPage("Challenge", "Post", new { provider = this.ExternalLoginScheme, returnUrl });
            }

            return this.Page();
        }

        public async Task<IActionResult> OnPostAsync(string button)
        {
            // check if we are in the context of an authorization request
            var context = await this.interaction.GetAuthorizationContextAsync(this.Input.ReturnUrl);

            // the user clicked the "cancel" button
            if (button != "login")
            {
                if (context != null)
                {
                    // if the user cancels, send a result back into IdentityServer as if they 
                    // denied the consent (even if this client does not require consent).
                    // this will send back an access denied OIDC error response to the client.
                    await this.interaction.DenyAuthorizationAsync(context, AuthorizationError.AccessDenied);

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    if (context.IsNativeClient())
                    {
                        // if the client is PKCE then we assume it's native, so this change in how to
                        // return the response is for better UX for the end user.
                        return this.RedirectToPage("Redirect", new { returnUrl = this.Input.ReturnUrl });
                    }

                    return this.Redirect(this.Input.ReturnUrl);
                }

                // since we don't have a valid context, then we just go back to the home page
                return this.Redirect("~/");
            }

            if (this.ModelState.IsValid)
            {
                // This does count login failures towards account lockout
                // To disable password failures to trigger account lockout, set lockoutOnFailure: false
                var result = await this.signInManager.PasswordSignInAsync(this.Input.Email, this.Input.Password, this.Input.RememberMe, true);
                if (result.Succeeded)
                {
                    this.logger.LogInformation("User logged in.");

                    var user = await this.userManager.FindByEmailAsync(this.Input.Email);
                    await this.events.RaiseAsync(new UserLoginSuccessEvent(IdentityServerConstants.LocalIdentityProvider, user.Id.ToString(), user.UserName, clientId: context?.Client.ClientId));

                    if (context != null)
                    {
                        if (context.IsNativeClient())
                        {
                            // if the client is PKCE then we assume it's native, so this change in how to
                            // return the response is for better UX for the end user.
                            return this.RedirectToPage("Redirect", new { this.Input.ReturnUrl });
                        }

                        // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                        return this.Redirect(this.Input.ReturnUrl);
                    }

                    // request for a local page
                    if (this.Url.IsLocalUrl(this.Input.ReturnUrl))
                    {
                        return this.Redirect(this.Input.ReturnUrl);
                    }

                    if (string.IsNullOrEmpty(this.Input.ReturnUrl))
                    {
                        return this.Redirect("~/");
                    }

                    // user might have clicked on a malicious link - should be logged
                    this.logger.LogWarning("Invalid return URL registered: {url}", this.Input.ReturnUrl);
                    return this.Redirect("~/");
                }

                if (result.RequiresTwoFactor)
                {
                    return this.RedirectToPage("./LoginWith2fa", new { this.Input.ReturnUrl, this.Input.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    this.logger.LogWarning("User account locked out.");
                    return this.RedirectToPage("./Lockout");
                }

                await this.events.RaiseAsync(new UserLoginFailureEvent(this.Input.Email, "invalid credentials", clientId: context?.Client.ClientId));
                this.ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                await this.BuildLoginViewModelAsync(this.Input.ReturnUrl);
                return this.Page();
            }

            // If we got this far, something failed, redisplay form
            await this.BuildLoginViewModelAsync(this.Input.ReturnUrl);
            return this.Page();
        }

        private async Task BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await this.interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null)
            {
                var provider = await this.schemeProvider.GetSchemeAsync(context.IdP);
                if (provider != null)
                {
                    var local = context.IdP == IdentityServerConstants.LocalIdentityProvider;

                    // this is meant to short circuit the UI and only trigger the one external IdP
                    this.EnableLocalLogin = local;
                    this.Input.ReturnUrl = returnUrl;
                    this.Input.Email = context.LoginHint;


                    if (!local)
                    {
                        this.ExternalLogins = new[] { provider };
                    }
                }
            }
            else
            {
                var schemes = await this.schemeProvider.GetAllSchemesAsync();

                var providers = schemes
                    .Where(x => x.DisplayName != null)
                    .ToList();

                var allowLocal = true;
                if (context?.Client.ClientId != null)
                {
                    var client = await this.clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                    if (client != null)
                    {
                        allowLocal = client.EnableLocalLogin;

                        if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                        {
                            providers = providers.Where(provider =>
                                client.IdentityProviderRestrictions.Contains(provider.Name)).ToList();
                        }
                    }
                }


                this.EnableLocalLogin = allowLocal;
                this.Input.ReturnUrl = returnUrl;
                this.Input.Email = context?.LoginHint;
                this.ExternalLogins = providers.ToArray();
            }
        }

        public class InputModel
        {
            [Required] [EmailAddress] public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")] public bool RememberMe { get; set; }

            public string ReturnUrl { get; set; }
        }
    }
}