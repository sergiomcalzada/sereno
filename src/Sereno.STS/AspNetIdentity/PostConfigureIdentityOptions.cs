using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Sereno.STS.AspNetIdentity
{
    public class PostConfigureIdentityOptions : IPostConfigureOptions<IdentityOptions>
    {
        public void PostConfigure(string name, IdentityOptions options)
        {
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters = options.User.AllowedUserNameCharacters + "\\" + "#";
#if DEBUG
            options.SignIn.RequireConfirmedAccount = true;
#endif
        }
    }
}
