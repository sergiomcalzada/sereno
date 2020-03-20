using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sereno.Domain.Entity;
using Sereno.STS.AspNetIdentity;
using Sereno.STS.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class AspNetIdentityExtensions
    {
        public static IServiceCollection AddAspNetIdentity(this IServiceCollection services)
        {

            services.AddIdentity<User, Role>()
                .AddUserStore<UserStore>()
                .AddUserManager<UserManager>()
                .AddRoleStore<RoleStore>()
                .AddRoleManager<RoleManager>()
                .AddSignInManager<SignInManager>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserClaimsPrincipalFactory<User>, UserClaimsPrincipalFactory>();
            services.AddScoped<ISecurityStampValidator, Sereno.STS.AspNetIdentity.SecurityStampValidator>();
            services.AddSingleton<IPostConfigureOptions<IdentityOptions>, PostConfigureIdentityOptions>();

            return services;
        }
    }


}