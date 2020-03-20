using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using Sereno.Domain.EntityFramework;

namespace Sereno.STS.IdentityServer
{
    public class ClientStore : IClientStore
    {
        private readonly DataContext dbContext;

        public ClientStore(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = await this.dbContext
                .Set<Domain.Entity.Client>()
                .Where(x => x.ClientId == clientId)
                .Select(c => new Client()
                {
                    AbsoluteRefreshTokenLifetime =  c.AbsoluteRefreshTokenLifetime,
                    AccessTokenLifetime = c.AccessTokenLifetime,
                    AccessTokenType = (AccessTokenType) c.AccessTokenType,
                    AllowAccessTokensViaBrowser = c.AllowAccessTokensViaBrowser,
                    AllowOfflineAccess = c.AllowOfflineAccess,
                    AllowPlainTextPkce = c.AllowPlainTextPkce,
                    AllowRememberConsent = c.AllowRememberConsent,
                    AllowedCorsOrigins = c.AllowedCorsOrigins.Select(x => x.Origin).ToList(),
                    AllowedGrantTypes = c.AllowedGrantTypes.Select(x => x.GrantType).ToList(),
                    AllowedScopes = c.AllowedScopes.Select(x => x.Scope).ToList(),
                    AlwaysIncludeUserClaimsInIdToken = c.AlwaysIncludeUserClaimsInIdToken,
                    AlwaysSendClientClaims = c.AlwaysSendClientClaims,
                    AuthorizationCodeLifetime = c.AuthorizationCodeLifetime,
                    BackChannelLogoutSessionRequired = c.BackChannelLogoutSessionRequired,
                    BackChannelLogoutUri = c.BackChannelLogoutUri,
                    Claims = c.Claims.Select(x => new Claim(x.Type,x.Value)).ToList(),
                    ClientId = c.ClientId,
                    ClientClaimsPrefix = c.ClientClaimsPrefix,
                    ClientName = c.ClientName,
                    ClientSecrets = c.ClientSecrets.Select(x => new Secret(x.Value,x.Description,x.Expiration)).ToList(),
                    Description = c.Description,
                    ClientUri = c.ClientUri,
                    ConsentLifetime = c.ConsentLifetime,
                    DeviceCodeLifetime = c.DeviceCodeLifetime,
                    EnableLocalLogin = c.EnableLocalLogin,
                    Enabled = c.Enabled,
                    FrontChannelLogoutSessionRequired = c.FrontChannelLogoutSessionRequired,
                    FrontChannelLogoutUri = c.FrontChannelLogoutUri,
                    IdentityProviderRestrictions = c.IdentityProviderRestrictions.Select(x => x.Provider).ToList(),
                    IdentityTokenLifetime = c.IdentityTokenLifetime,
                    IncludeJwtId = c.IncludeJwtId,
                    LogoUri = c.LogoUri,
                    PairWiseSubjectSalt = c.PairWiseSubjectSalt,
                    PostLogoutRedirectUris = c.PostLogoutRedirectUris.Select(x => x.Uri).ToList(),
                    //Properties = null,
                    ProtocolType = c.ProtocolType,
                    RedirectUris = c.RedirectUris.Select(x => x.Uri).ToList(),
                    RefreshTokenExpiration = (TokenExpiration) c.RefreshTokenExpiration,
                    RefreshTokenUsage = (TokenUsage) c.RefreshTokenUsage,
                    RequireClientSecret = c.RequireClientSecret,
                    RequireConsent = c.RequireConsent,
                    RequirePkce = c.RequirePkce,
                    SlidingRefreshTokenLifetime = c.SlidingRefreshTokenLifetime,
                    UpdateAccessTokenClaimsOnRefresh = c.UpdateAccessTokenClaimsOnRefresh,
                    UserCodeType = c.UserCodeType,
                    UserSsoLifetime = c.UserSsoLifetime
                    
                })
                .FirstOrDefaultAsync();
            return client;
        }
    }
}