using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Sereno.Admin.Host.Options.OAuth2
{
    public static class OAuth2ServiceCollectionExtensions
    {
        public static void AddOAuth2(this IServiceCollection services)
        {
            services.AddOptions<OAuth2Options>()
                .Configure<IConfiguration>((settings, config) =>
                    {
                        config.GetSection(OAuth2Options.Section).Bind(settings);
                    });

            services.AddSingleton<IPostConfigureOptions<JwtBearerOptions>, JwtBearerOptionsConfigure>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer();
            
        }
    }
}