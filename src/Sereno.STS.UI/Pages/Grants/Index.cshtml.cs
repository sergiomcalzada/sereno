using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;


namespace Sereno.STS.UI.Pages.Grants
{
    public class IndexModel : PageModel
    {
        private readonly IEventService _events;
        private readonly IClientStore _clients;
        private readonly IResourceStore _resources;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly ILogger<IndexModel> _logger;

        public IEnumerable<GrantViewModel> Grants { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IIdentityServerInteractionService interaction,
            IEventService events,
            IClientStore clients, IResourceStore resources)
        {
            this._logger = logger;
            this._interaction = interaction;
            this._events = events;
            this._clients = clients;
            this._resources = resources;
        }

       

        public async Task<IActionResult> OnGetAsync()
        {
            await this.BuildViewModelAsync();
            return this.Page();
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostAsync(string clientId)
        {
            await this._interaction.RevokeUserConsentAsync(clientId);
            await this._events.RaiseAsync(new GrantsRevokedEvent(this.User.GetSubjectId(), clientId));

            return this.RedirectToPage("Index");
        }

        private async Task BuildViewModelAsync()
        {
            var grants = await this._interaction.GetAllUserGrantsAsync();

            var list = new List<GrantViewModel>();
            foreach(var grant in grants)
            {
                var client = await this._clients.FindClientByIdAsync(grant.ClientId);
                if (client != null)
                {
                    var resources = await this._resources.FindResourcesByScopeAsync(grant.Scopes);

                    var item = new GrantViewModel()
                    {
                        ClientId = client.ClientId,
                        ClientName = client.ClientName ?? client.ClientId,
                        ClientLogoUrl = client.LogoUri,
                        ClientUrl = client.ClientUri,
                        Description = grant.Description,
                        Created = grant.CreationTime,
                        Expires = grant.Expiration,
                        IdentityGrantNames = resources.IdentityResources.Select(x => x.DisplayName ?? x.Name).ToArray(),
                        ApiGrantNames = resources.ApiScopes.Select(x => x.DisplayName ?? x.Name).ToArray()
                    };

                    list.Add(item);
                }
            }

            this.Grants = list;
        }

        
        public class GrantViewModel
        {
            public string ClientId { get; set; }
            public string ClientName { get; set; }
            public string ClientUrl { get; set; }
            public string ClientLogoUrl { get; set; }
            public string Description { get; set; }
            public DateTime Created { get; set; }
            public DateTime? Expires { get; set; }
            public IEnumerable<string> IdentityGrantNames { get; set; }
            public IEnumerable<string> ApiGrantNames { get; set; }
        }
    }
}