using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using Sereno.Domain.Entity;
using Sereno.STS.UI;


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
            
            //services.AddSingleton<IPostConfigureOptions<StaticFileOptions>, UIConfigureOptions>();
            return builder;
        }
    }


}