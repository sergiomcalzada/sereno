using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Sereno.STS.UI.Pages.Diagnostics
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public AuthenticateResult AuthenticateResult { get; set; }
        public IEnumerable<string> Clients { get; set; } = new List<string>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            this._logger = logger;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            var localAddresses = new string[] { "127.0.0.1", "::1", this.HttpContext.Connection.LocalIpAddress.ToString() };
            if (!localAddresses.Contains(this.HttpContext.Connection.RemoteIpAddress.ToString()))
            {
                return this.NotFound();
            }


            await this.BuildViewModelAsync();
            return this.Page();
        }

        private async Task BuildViewModelAsync()
        {
            var result = await this.HttpContext.AuthenticateAsync();
            this.AuthenticateResult = result;
            if (result.Properties.Items.ContainsKey("client_list"))
            {
                var encoded = result.Properties.Items["client_list"];
                var bytes = Base64Url.Decode(encoded);
                var value = Encoding.UTF8.GetString(bytes);

                this.Clients = JsonConvert.DeserializeObject<string[]>(value);
            }
        }
    }
}