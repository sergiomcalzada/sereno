using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;



namespace Sereno.STS.UI.Pages.Device
{
    public class SuccessModel : PageModel
    {
        private readonly ILogger<SuccessModel> _logger;

        public SuccessModel(ILogger<SuccessModel> logger)
        {
            this._logger = logger;
        }


        public IActionResult OnGet()
        {
            return this.Page();
        }

    }
}