using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

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
            
            return builder;
        }
    }


}