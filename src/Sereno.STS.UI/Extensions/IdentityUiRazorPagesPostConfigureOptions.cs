using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Sereno.STS.UI.Extensions
{
    public class IdentityUiRazorPagesPostConfigureOptions : IPostConfigureOptions<RazorPagesOptions>
    {
        public void PostConfigure(string name, RazorPagesOptions options)
        {
            options.Conventions.AuthorizeFolder("/Account/Manage");
            options.Conventions.AuthorizePage("/Account/Logout");

            options.Conventions.AuthorizeFolder("/Consent");
            options.Conventions.AuthorizeFolder("/Grants");
            options.Conventions.AuthorizeFolder("/Diagnostics");
        }
    }
}
