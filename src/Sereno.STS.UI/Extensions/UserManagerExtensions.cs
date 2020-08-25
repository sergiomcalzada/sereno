using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Sereno.Domain.Entity;

namespace Sereno.STS.UI.Extensions
{
    public static class UserManagerExtensions
    {
        public static async Task<User> AutoProvisionUser(this UserManager<User> manager, string provider, string providerUserId, IEnumerable<Claim> claims)
        {

            var user = new User()
            {

            };

            var createResult = await manager.CreateAsync(user);
            if (createResult.Succeeded)
            {
                var addLoginResult = await manager.AddLoginAsync(user,new UserLoginInfo(provider,providerUserId, user.UserName));
                if (addLoginResult.Succeeded)
                {
                    return user;
                }
            }

            return null;
        }
    }
}