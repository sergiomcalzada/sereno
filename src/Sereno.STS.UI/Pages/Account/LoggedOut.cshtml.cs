using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Sereno.STS.UI.Pages.Account
{
    public class LoggedOutModel : PageModel
    {
        private readonly IIdentityServerInteractionService interaction;
        private readonly ILogger<LogoutModel> logger;

        public LoggedOutModel(ILogger<LogoutModel> logger,
            IIdentityServerInteractionService interaction)
        {
            this.logger = logger;
            this.interaction = interaction;
        }

        public bool AutomaticRedirectAfterSignOut { get; set; }

        public object LogoutId { get; set; }

        public string SignOutIframeUrl { get; set; }

        public string ClientName { get; set; }

        public string PostLogoutRedirectUri { get; set; }

        public async Task<IActionResult> OnGet(string logoutId)
        {
            await this.BuildLoggedOutViewModelAsync(logoutId);
            return this.Page();
        }

        private async Task BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await this.interaction.GetLogoutContextAsync(logoutId);

            this.AutomaticRedirectAfterSignOut = true;
            this.PostLogoutRedirectUri = logout?.PostLogoutRedirectUri;
            this.ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName;
            this.SignOutIframeUrl = logout?.SignOutIFrameUrl;
            this.LogoutId = logoutId;
        }
    }
}