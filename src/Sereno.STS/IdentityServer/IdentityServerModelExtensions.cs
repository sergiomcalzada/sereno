using System;
using System.Linq;
using System.Security.Claims;
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
                    AllowedScopes = c.AllowedScopes.Select(x => x.Scope).ToList(),
                    AlwaysIncludeUserClaimsInIdToken = c.AlwaysIncludeUserClaimsInIdToken,
                    AlwaysSendClientClaims = c.AlwaysSendClientClaims,
                    AuthorizationCodeLifetime = c.AuthorizationCodeLifetime,
                    BackChannelLogoutSessionRequired = c.BackChannelLogoutSessionRequired,
                    BackChannelLogoutUri = c.BackChannelLogoutUri,
                    Claims = c.Claims.Select(x => new Claim(x.Type, x.Value)).ToList(),
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
                    IdentityProviderRestrictions = c.IdentityProviderRestrictions.Select(x => x.Provider).ToList(),
                    IdentityTokenLifetime = c.IdentityTokenLifetime,
                    IncludeJwtId = c.IncludeJwtId,
                    LogoUri = c.LogoUri,
                    PairWiseSubjectSalt = c.PairWiseSubjectSalt,
                    PostLogoutRedirectUris = c.PostLogoutRedirectUris.Select(x => x.Uri).ToList(),
                    //Properties = null,
                    ProtocolType = c.ProtocolType,
                    RedirectUris = c.RedirectUris.Select(x => x.Uri).ToList(),
                    RefreshTokenExpiration = (TokenExpiration)c.RefreshTokenExpiration,
                    RefreshTokenUsage = (TokenUsage)c.RefreshTokenUsage,
                    RequireClientSecret = c.RequireClientSecret,
                    RequireConsent = c.RequireConsent,
                    RequirePkce = c.RequirePkce,
                    SlidingRefreshTokenLifetime = c.SlidingRefreshTokenLifetime,
                    UpdateAccessTokenClaimsOnRefresh = c.UpdateAccessTokenClaimsOnRefresh,
                    UserCodeType = c.UserCodeType,
                    UserSsoLifetime = c.UserSsoLifetime
                });
        }

        public static IQueryable<IdentityResource> AsModel(this IQueryable<Domain.Entity.IdentityResource> query)
        {
            return query.Select(r => new IdentityResource()
            {
                Name = r.Name,
                Enabled = r.Enabled,
                //Properties = null,
                Description = r.Description,
                DisplayName = r.DisplayName,
                UserClaims = r.UserClaims.Select(x => x.Type).ToList(),
                Emphasize = r.Emphasize,
                Required = r.Required,
                ShowInDiscoveryDocument = r.ShowInDiscoveryDocument
            });
        }

        public static IQueryable<ApiResource> AsModel(this IQueryable<Domain.Entity.ApiResource> query)
        {
            return query.Select(r => new ApiResource()
            {
                Description = r.Description,
                Enabled = r.Enabled,
                Name = r.Name,
                //Properties = null,
                ApiSecrets = r.Secrets.Select(x => new Secret(x.Value, x.Description, x.Expiration)).ToList(),
                DisplayName = r.DisplayName,
                Scopes = r.Scopes.Select(x => new Scope($"{r.Name}/{x.Name}", x.DisplayName, x.UserClaims.Select(uc => uc.Type))).ToList(),
                UserClaims = r.UserClaims.Select(x => x.Type).ToList()
            });
        }
    }
}