using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sereno.STS.UI.Pages
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            return this.Page();
        }
    }
}