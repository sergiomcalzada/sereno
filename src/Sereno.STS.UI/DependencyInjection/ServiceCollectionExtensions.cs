using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sereno.Domain.Entity;
using Sereno.STS.UI;


namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUI(this IServiceCollection services)
        {
            
            //services.AddSingleton<IPostConfigureOptions<StaticFileOptions>, UIConfigureOptions>();
            return services;
        }
    }


}