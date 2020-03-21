using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sereno.Domain.Entity;
using Sereno.STS.IdentityServer;

namespace Sereno.STS.AspNetIdentity
{
    public class UserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public UserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<Role> roleManager,
            IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);
            var identity = principal.Identities.First();

            var username = await this.UserManager.GetUserNameAsync(user);

            this.EnsureNameClaim(identity, username);

            await this.EnsureEmailClaim(user, identity);

            await this.EnsurePhoneNumberClaim(user, identity);

            return principal;
        }

        private void EnsureNameClaim(ClaimsIdentity identity, string username)
        {
            if (!identity.HasClaim(x => x.Type == JwtClaimTypes.Name))
            {
                identity.AddClaim(new Claim(JwtClaimTypes.Name, username));
            }
        }

        private async Task EnsureEmailClaim(User user, ClaimsIdentity identity)
        {
            if (this.UserManager.SupportsUserEmail)
            {
                var email = await this.UserManager.GetEmailAsync(user);

                if (!string.IsNullOrWhiteSpace(email))
                {
                    var isEmailConfirmed = await this.UserManager.IsEmailConfirmedAsync(user);
                    identity.AddClaims(new[]
                    {
                        new Claim(JwtClaimTypes.Email, email),
                        new Claim(JwtClaimTypes.EmailVerified, isEmailConfirmed.ToString().ToLower(),
                            ClaimValueTypes.Boolean)
                    });
                }
            }
        }

        private async Task EnsurePhoneNumberClaim(User user, ClaimsIdentity identity)
        {
            if (this.UserManager.SupportsUserPhoneNumber)
            {
                var phoneNumber = await this.UserManager.GetPhoneNumberAsync(user);

                if (!string.IsNullOrWhiteSpace(phoneNumber))
                {
                    var isPhoneNumberConfirmed = await this.UserManager.IsPhoneNumberConfirmedAsync(user);
                    identity.AddClaims(new[]
                    {
                        new Claim(JwtClaimTypes.PhoneNumber, phoneNumber),
                        new Claim(JwtClaimTypes.PhoneNumberVerified,isPhoneNumberConfirmed.ToString().ToLower(),ClaimValueTypes.Boolean)
                    });
                }
            }
        }
    }
}