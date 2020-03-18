# sereno

Sereno is a full opensource implementation of an OpenId/OAuth2 server using IdentityServer4 libraries and DotNetCore3.1


# Feature (wish) list

* Mixed AspNet Identity and Identity Server model extended with
  * Groups
  * Roles per Api Resource
* User profile page configration
  * Change password
  * Configure MFA
* Multi database provider support
* Full administration panel
* Dynamic External OpenId Provider (without restart)
* Dynamic External REST Provider (without restart)
* Impersonation for administrators 
* Webhooks
* Dynamic Client Registration [RFC 7591](https://github.com/IdentityServer/IdentityServer4/issues/1248)
* Custom grants/flows
  * Resource Owner Password
  * On behalf of
  * Temporary user access tokens (for _anonymous access_)
* Docker support, with scaling.
