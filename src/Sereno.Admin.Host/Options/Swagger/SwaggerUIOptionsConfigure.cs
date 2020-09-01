﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Sereno.Admin.Host.Options.OAuth2;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Sereno.Admin.Host.Options.Swagger
{
    public class SwaggerUIOptionsConfigure : IConfigureOptions<SwaggerUIOptions>
    {
        private readonly SwaggerOAuth2Options swaggerOAuth2Options;
        private readonly OAuth2Options oauth2Options;

        public SwaggerUIOptionsConfigure(
            IOptions<SwaggerOAuth2Options> swaggerOAuth2Options,
            IOptions<OAuth2Options> oauth2Options)
        {
            this.swaggerOAuth2Options = swaggerOAuth2Options.Value;
            this.oauth2Options = oauth2Options.Value;
        }
        public void Configure(SwaggerUIOptions options)
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");

            if (this.oauth2Options.IsEnabled)
            {
                options.OAuthClientId(this.swaggerOAuth2Options.ClientId);
                options.OAuthRealm(this.oauth2Options.Application);
                options.OAuthAppName(this.swaggerOAuth2Options.ApplicationName);
                options.OAuthScopeSeparator(" ");
            }
        }
    }
}