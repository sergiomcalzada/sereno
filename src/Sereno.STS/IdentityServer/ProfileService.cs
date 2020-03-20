using System.Threading.Tasks;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Sereno.Domain.Entity;

namespace Sereno.STS.IdentityServer
{
    public class ProfileService : ProfileService<User>
    {
        public ProfileService(UserManager<User> userManager, IUserClaimsPrincipalFactory<User> claimsFactory) : base(
            userManager, claimsFactory)
        {
        }

        public ProfileService(UserManager<User> userManager, IUserClaimsPrincipalFactory<User> claimsFactory,
            ILogger<ProfileService<User>> logger) : base(userManager, claimsFactory, logger)
        {
        }

        public override Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            return base.GetProfileDataAsync(context);
        }
    }
}