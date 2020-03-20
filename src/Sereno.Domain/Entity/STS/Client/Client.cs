using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sereno.Domain.Entity
{
    public class Client
    {
        public string Id { get; set; }
        public bool Enabled { get; set; } = true;
        public string ClientId { get; set; }
        public string ProtocolType { get; set; } = ProtocolTypes.OpenIdConnect;
        public ICollection<ClientSecret> ClientSecrets { get; set; } = new Collection<ClientSecret>();
        public bool RequireClientSecret { get; set; } = true;
        public string ClientName { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }
        public bool RequireConsent { get; set; } = true;
        public bool AllowRememberConsent { get; set; } = true;
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }
        public ICollection<ClientGrantType> AllowedGrantTypes { get; set; } = new Collection<ClientGrantType>();
        public bool RequirePkce { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }
        public ICollection<ClientRedirectUri> RedirectUris { get; set; } = new Collection<ClientRedirectUri>();
        public ICollection<ClientPostLogoutRedirectUri> PostLogoutRedirectUris { get; set; } = new Collection<ClientPostLogoutRedirectUri>();
        //public string LogoutUri { get; set; }
        public string BackChannelLogoutUri { get; set; }
        public string FrontChannelLogoutUri { get; set; }
        //public bool LogoutSessionRequired { get; set; } = true;
        public bool BackChannelLogoutSessionRequired { get; set; } = true;
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;
        public bool AllowOfflineAccess { get; set; }
        public ICollection<ClientScope> AllowedScopes { get; set; }
        public int IdentityTokenLifetime { get; set; } = 3600;
        public int AccessTokenLifetime { get; set; } = 1800;
        public int AuthorizationCodeLifetime { get; set; } = 300;
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;
        public TokenUsage RefreshTokenUsage { get; set; } = TokenUsage.OneTimeOnly;
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public TokenExpiration RefreshTokenExpiration { get; set; } = TokenExpiration.Absolute;
        public AccessTokenType AccessTokenType { get; set; } = AccessTokenType.Jwt;
        public bool EnableLocalLogin { get; set; } = true;
        public ICollection<ClientIdPRestriction> IdentityProviderRestrictions { get; set; } = new Collection<ClientIdPRestriction>();
        public bool IncludeJwtId { get; set; }
        public ICollection<ClientClaim> Claims { get; set; } = new Collection<ClientClaim>();
        public bool AlwaysSendClientClaims { get; set; }
        public bool PrefixClientClaims { get; set; } = true;
        public string ClientClaimsPrefix { get; set; }
        public ICollection<ClientCorsOrigin> AllowedCorsOrigins { get; set; } = new Collection<ClientCorsOrigin>();
        public int? ConsentLifetime { get; set; }
        public string PairWiseSubjectSalt { get; set; }
        public string UserCodeType { get; set; }
        public int? UserSsoLifetime { get; set; }
        public string Description { get; set; }
        public int DeviceCodeLifetime { get; set; }
    }
}
