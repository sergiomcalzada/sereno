using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;


namespace Sereno.STS.UI.Pages.Device
{
    public class IndexModel : PageModel
    {
        private readonly IOptions<IdentityServerOptions> _options;
        private readonly ILogger<IndexModel> _logger;


        public IndexModel(ILogger<IndexModel> logger, IOptions<IdentityServerOptions> options)
        {
            this._logger = logger;
            this._options = options;
        }


        [BindProperty] public InputModel Input { get; set; } = new InputModel();
        [TempData] public string ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            var userCodeParamName = this._options.Value.UserInteraction.DeviceVerificationUserCodeParameter;
            string userCode = this.Request.Query[userCodeParamName];
            if (string.IsNullOrWhiteSpace(userCode))
            {
                return this.Page();
            }

            return this.RedirectToPage("UserCodeConfirmation", userCode);
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPost()
        {
            var model = this.Input;
            string userCode = model.UserCode;
            if (string.IsNullOrWhiteSpace(userCode))
            {
                return this.Page();
            }

            return this.RedirectToPage("UserCodeConfirmation", userCode);
        }


        public class InputModel
        {
            public string UserCode { get; set; }
        }

    }
}