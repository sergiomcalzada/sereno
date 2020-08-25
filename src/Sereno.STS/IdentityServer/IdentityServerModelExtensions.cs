using System.Collections.Generic;
using System.Linq;
using IdentityServer4.Models;

namespace Sereno.STS.IdentityServer
{
    public static class IdentityServerModelExtensions
    {
        public static IQueryable<Client> AsModel(this IQueryable<Domain.Entity.Client> query)
        {
            return query
                .Select(c => new Client
                {
                    AbsoluteRefreshTokenLifetime = c.AbsoluteRefreshTokenLifetime,
                    AccessTokenLifetime = c.AccessTokenLifetime,
                    AccessTokenType = (AccessTokenType)c.AccessTokenType,
                    AllowAccessTokensViaBrowser = c.AllowAccessTokensViaBrowser,
                    AllowOfflineAccess = c.AllowOfflineAccess,
                    AllowPlainTextPkce = c.AllowPlainTextPkce,
                    AllowRememberConsent = c.AllowRememberConsent,
                    AllowedCorsOrigins = c.AllowedCorsOrigins.Select(x => x.Origin).ToList(),
                    AllowedGrantTypes = c.AllowedGrantTypes.Select(x => x.GrantType).ToList(),
                    AllowedScopes = c.AllowedScopes.Select(x => x.ApiScope.Name).ToList(),
                    AlwaysIncludeUserClaimsInIdToken = c.AlwaysIncludeUserClaimsInIdToken,
                    AlwaysSendClientClaims = c.AlwaysSendClientClaims,
                    AuthorizationCodeLifetime = c.AuthorizationCodeLifetime,
                    BackChannelLogoutSessionRequired = c.BackChannelLogoutSessionRequired,
                    BackChannelLogoutUri = c.BackChannelLogoutUri,
                    Claims = c.Claims.Select(x => new ClientClaim(x.Type, x.Value)).ToList(),
                    ClientId = c.ClientId,
                    ClientClaimsPrefix = c.ClientClaimsPrefix,
                    ClientName = c.ClientName,
                    ClientSecrets = c.ClientSecrets.Select(x => new Secret(x.Value, x.Description, x.Expiration)).ToList(),
                    Description = c.Description,
                    ClientUri = c.ClientUri,
                    ConsentLifetime = c.ConsentLifetime,
                    DeviceCodeLifetime = c.DeviceCodeLifetime,
                    EnableLocalLogin = c.EnableLocalLogin,
                    Enabled = c.Enabled,
                    FrontChannelLogoutSessionRequired = c.FrontChannelLogoutSessionRequired,
                    FrontChannelLogoutUri = c.FrontChannelLogoutUri,
                    IdentityProviderRestrictions = c.IdentityProviderRestrictions.Select(x => x.IdentityProvider.Name).ToList(),
                    IdentityTokenLifetime = c.IdentityTokenLifetime,
                    IncludeJwtId = c.IncludeJwtId,
                    LogoUri = c.LogoUri,
                    PairWiseSubjectSalt = c.PairWiseSubjectSalt,
                    PostLogoutRedirectUris = c.PostLogoutRedirectUris.Select(x => x.PostLogoutRedirectUri).ToList(),
                    ProtocolType = c.ProtocolType,
                    RedirectUris = c.RedirectUris.Select(x => x.RedirectUri).ToList(),
                    RefreshTokenExpiration = (TokenExpiration)c.RefreshTokenExpiration,
                    RefreshTokenUsage = (TokenUsage)c.RefreshTokenUsage,
                    RequireClientSecret = c.RequireClientSecret,
                    RequireConsent = c.RequireConsent,
                    RequirePkce = c.RequirePkce,
                    RequireRequestObject = c.RequireRequestObject,
                    SlidingRefreshTokenLifetime = c.SlidingRefreshTokenLifetime,
                    UpdateAccessTokenClaimsOnRefresh = c.UpdateAccessTokenClaimsOnRefresh,
                    UserCodeType = c.UserCodeType,
                    UserSsoLifetime = c.UserSsoLifetime,
                    AllowedIdentityTokenSigningAlgorithms = c.AllowedIdentityTokenSigningAlgorithms.Select(x => x.SecurityAlgorithm.ToString()).ToList(),
                    Properties = (IDictionary<string, string>)c.Properties.Select(p => new KeyValuePair<string, string>(p.Key, p.Value)),
                });
        }

        public static IQueryable<IdentityResource> AsModel(this IQueryable<Domain.Entity.IdentityResource> query)
        {
            return query.Select(r => new IdentityResource
            {
                Name = r.Name,
                Enabled = r.Enabled,
                Description = r.Description,
                DisplayName = r.DisplayName,
                UserClaims = r.UserClaims.Select(x => x.Type).ToList(),
                Emphasize = r.Emphasize,
                Required = r.Required,
                ShowInDiscoveryDocument = r.ShowInDiscoveryDocument,
                Properties = (IDictionary<string, string>)r.Properties.Select(p => new KeyValuePair<string, string>(p.Key, p.Value)),
            });
        }

        public static IQueryable<ApiResource> AsModel(this IQueryable<Domain.Entity.ApiResource> query)
        {
            return query.Select(r => new ApiResource
            {
                Description = r.Description,
                Enabled = r.Enabled,
                Name = r.Name,
                ApiSecrets = r.Secrets.Select(x => new Secret(x.Value, x.Description, x.Expiration)).ToList(),
                DisplayName = r.DisplayName,
                Scopes = r.Scopes.Select(x => x.ApiScope.Name).ToList(),
                ShowInDiscoveryDocument = r.ShowInDiscoveryDocument,
                UserClaims = r.UserClaims.Select(x => x.Type).ToList(),
                AllowedAccessTokenSigningAlgorithms = r.AllowedAccessTokenSigningAlgorithms.Select(x => x.SecurityAlgorithm.ToString()).ToList(),
                Properties = (IDictionary<string, string>)r.Properties.Select(p => new KeyValuePair<string, string>(p.Key, p.Value)),
            });
        }

        public static IQueryable<ApiScope> AsModel(this IQueryable<Domain.Entity.ApiScope> query)
        {
            return query.Select(r => new ApiScope
            {
                Description = r.Description,
                Enabled = r.Enabled,
                Name = r.Name,
                DisplayName = r.DisplayName,
                Emphasize = r.Emphasize,
                Required = r.Required,
                ShowInDiscoveryDocument = r.ShowInDiscoveryDocument,
                UserClaims = r.UserClaims.Select(x => x.Type).ToList(),
                Properties = (IDictionary<string, string>)r.Properties.Select(p => new KeyValuePair<string, string>(p.Key, p.Value)),
            });
        }
    }
}