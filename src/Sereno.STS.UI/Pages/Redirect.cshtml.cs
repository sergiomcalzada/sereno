using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sereno.STS.UI.Pages
{
    public class RedirectModel : PageModel
    {
        public string RedirectUrl { get; set; }

        public IActionResult OnGet(string redirectUrl)
        {
            this.RedirectUrl = redirectUrl;
            return this.Page();
        }
    }
}
