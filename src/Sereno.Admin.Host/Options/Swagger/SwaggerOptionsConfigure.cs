using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

namespace Sereno.Admin.Host.Options.Swagger
{
    public class SwaggerOptionsConfigure : IConfigureOptions<SwaggerOptions>
    {
        public void Configure(SwaggerOptions options)
        {            
            options.PreSerializeFilters.Add((apiDocument, httpReq) =>
            {
                
            });
        }
    }
}