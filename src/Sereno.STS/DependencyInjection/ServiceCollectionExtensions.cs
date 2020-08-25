using System.IO;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Sereno.STS.UI.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddUI(this IMvcBuilder builder)
        {

#if DEBUG
            builder.AddRazorRuntimeCompilation(options =>
            {
                var libraryPath = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "..", "Sereno.STS.UI")); 
                options.FileProviders.Add(new PhysicalFileProvider(libraryPath)); 
            });

#endif
            builder.AddApplicationPart(typeof(IdentityUiRazorPagesPostConfigureOptions).Assembly);
            builder.Services
                .AddSingleton<IPostConfigureOptions<RazorPagesOptions>, IdentityUiRazorPagesPostConfigureOptions>();
            return builder;
        }
    }


}