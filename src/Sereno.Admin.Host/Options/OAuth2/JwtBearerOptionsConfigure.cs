using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Sereno.Admin.Host.Options.OAuth2
{

    public class JwtBearerOptionsConfigure : IPostConfigureOptions<JwtBearerOptions>
    {
        private readonly OAuth2Options oauth2Options;

        public JwtBearerOptionsConfigure(IOptions<OAuth2Options> options)
        {
            this.oauth2Options = options.Value;
        }

        public void PostConfigure(string name, JwtBearerOptions options)
        {
            options.Authority = this.oauth2Options.Authority;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuers = new[]
                {
                    this.oauth2Options.Authority
                },
                ValidAudiences = new[]
                {
                    this.oauth2Options.Authority + "/resources", this.oauth2Options.Application
                },
            };
            //options.BackchannelHttpHandler = new HttpClientHandler()
            //{
            //    Proxy = new WebProxy(proxyuri, true, new string[0], new NetworkCredential(userName, password, domain))
            //};
#if DEBUG
            options.RequireHttpsMetadata = false;
            options.IncludeErrorDetails = true;

            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = this.AuthenticationFailed
            };
#endif
        }

        private async Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
            // For debugging purposes only!
            var s = $"AuthenticationFailed: {arg.Exception.Message}";
            arg.Response.ContentLength = s.Length;
            await arg.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(s), 0, s.Length);
        }
    }
}