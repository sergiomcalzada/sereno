using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.EntityFrameworkCore;
using Sereno.Domain.EntityFramework;

namespace Sereno.STS.IdentityServer
{
    public class ResourceStore : IResourceStore
    {
        private readonly DataContext dbContext;

        public ResourceStore(DataContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var identity = await this.GetAllIdentityResources()
                .Where(r => scopeNames.Contains(r.Name))
                .ToArrayAsync();
            return identity;
        }

        public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            var scopes = await this.GetAllApiScopes()
                .Where(r => scopeNames.Contains(r.Name))
                .ToArrayAsync();
            return scopes;
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            var api = await this.GetAllApiResources()
                .Where(r => r.Scopes.Any(x => scopeNames.Contains(x)))
                .ToArrayAsync();
            return api;
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            var api = await this.GetAllApiResources()
                .Where(r => apiResourceNames.Contains(r.Name))
                .ToArrayAsync();
            return api;
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var identityResources = await this.GetAllIdentityResources().ToArrayAsync();
            var apiResources = await this.GetAllApiResources().ToArrayAsync();
            var apiScopes = await this.GetAllApiScopes().ToArrayAsync();
            return new Resources(identityResources, apiResources, apiScopes);
        }

        private IQueryable<IdentityResource> GetAllIdentityResources()
        {
            var identity = this.dbContext.Set<Domain.Entity.IdentityResource>().AsModel();
            return identity;
        }

        private IQueryable<ApiResource> GetAllApiResources()
        {
            var api = this.dbContext.Set<Domain.Entity.ApiResource>().AsModel();
            return api;
        }

        private IQueryable<ApiScope> GetAllApiScopes()
        {
            var api = this.dbContext.Set<Domain.Entity.ApiScope>().AsModel();
            return api;
        }
    }
}