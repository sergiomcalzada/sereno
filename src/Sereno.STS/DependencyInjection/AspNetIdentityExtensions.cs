using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Sereno.Domain.Entity;
using Sereno.STS.AspNetIdentity;

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

            services.AddSingleton<IEmailSender, EmptyEmailSender>();
            services.AddSingleton<IIdGenerator, IdGenerator>();
            return services;
        }
    }


}