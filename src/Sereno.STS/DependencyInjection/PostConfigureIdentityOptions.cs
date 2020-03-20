using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Sereno.STS.DependencyInjection
{
    public class PostConfigureIdentityOptions : IPostConfigureOptions<IdentityOptions>
    {
        public void PostConfigure(string name, IdentityOptions options)
        {
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = options.User.AllowedUserNameCharacters + "\\" + "#";
        }
    }
}
