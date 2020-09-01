using System.Collections.Generic;
using System.Linq;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Validation;

namespace Sereno.STS.UI.Pages.Shared
{
    public class ScopeModel
    {
        public string Value { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Emphasize { get; set; }
        public bool Required { get; set; }
        public bool Checked { get; set; }

        public static ScopeModel CreateScopeViewModel(IdentityResource identity, bool check)
        {
            return new ScopeModel
            {
                Value = identity.Name,
                DisplayName = identity.DisplayName ?? identity.Name,
                Description = identity.Description,
                Emphasize = identity.Emphasize,
                Required = identity.Required,
                Checked = check || identity.Required
            };
        }

        public static ScopeModel CreateScopeViewModel(ParsedScopeValue parsedScopeValue, ApiScope apiScope, bool check)
        {
            return new ScopeModel
            {
                Value = parsedScopeValue.RawValue,
                // todo: use the parsed scope value in the display?
                DisplayName = apiScope.DisplayName ?? apiScope.Name,
                Description = apiScope.Description,
                Emphasize = apiScope.Emphasize,
                Required = apiScope.Required,
                Checked = check || apiScope.Required
            };
        }

        public static ScopeModel GetOfflineAccessScope(bool check)
        {
            return new ScopeModel
            {
                Value = IdentityServer4.IdentityServerConstants.StandardScopes.OfflineAccess,
                DisplayName = ConsentOptions.OfflineAccessDisplayName,
                Description = ConsentOptions.OfflineAccessDescription,
                Emphasize = true,
                Checked = check
            };
        }

        public static IEnumerable<ScopeModel> GetIdentityResourcesFromRequest(ResourceValidationResult validatedResources, string[] scopesConsented, bool check)
        {
            return validatedResources.Resources.IdentityResources.Select(x =>
                ScopeModel.CreateScopeViewModel(x, scopesConsented.Contains(x.Name) || check)).ToArray();
        }

        public static IEnumerable<ScopeModel> GetApiScopeModelsFromRequest(ResourceValidationResult validatedResources, string[] scopesConsented, bool  check)
        {
            var apiScopes = new List<ScopeModel>();
            foreach (var parsedScope in validatedResources.ParsedScopes)
            {
                var apiScope = validatedResources.Resources.FindApiScope(parsedScope.ParsedName);
                if (apiScope != null)
                {
                    var scopeVm = ScopeModel.CreateScopeViewModel(parsedScope, apiScope, scopesConsented.Contains(parsedScope.RawValue) || check);
                    apiScopes.Add(scopeVm);
                }
            }

            if (ConsentOptions.EnableOfflineAccess && validatedResources.Resources.OfflineAccess)
            {
                apiScopes.Add(ScopeModel.GetOfflineAccessScope(
                    scopesConsented.Contains(IdentityServerConstants.StandardScopes.OfflineAccess) || check));
            }

            return apiScopes;
        }
    }
    //TODO: move outside
    public class ConsentOptions
    {
        public static bool EnableOfflineAccess = true;
        public static string OfflineAccessDisplayName = "Offline Access";
        public static string OfflineAccessDescription = "Access to your applications and resources, even when you are offline";

        public static readonly string MustChooseOneErrorMessage = "You must pick at least one permission";
        public static readonly string InvalidSelectionErrorMessage = "Invalid selection";
    }
}
