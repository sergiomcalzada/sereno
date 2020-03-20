using Sereno.Domain.Entity;
using Sereno.STS.IdentityServer;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IdentityServerExtensions
    {
        public static IServiceCollection AddIdentityServer4(this IServiceCollection services)
        {
            services.AddIdentityServer(options =>
                {
                    // configure identity server options
                    options.Events.RaiseSuccessEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseErrorEvents = true;
                })
                .AddClientStore<ClientStore>()
                .AddResourceStore<ResourceStore>()
                .AddProfileService<ProfileService>()
                .AddAspNetIdentity<User>();

            return services;
        }
    }
}