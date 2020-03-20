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

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var identity = await this.GetAllIdentityResources()
                .Where(r => scopeNames.Contains(r.Name))
                .ToArrayAsync();
            return identity;
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            var api = await this.GetAllApiResources()
                .Where(x => scopeNames.Contains(x.Name))
                .ToArrayAsync();

            return api;
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var api = await this.GetAllApiResources()
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();

            return api;
        }

        public async Task<Resources> GetAllResourcesAsync()
        {
            var identity = await this.GetAllIdentityResources().ToArrayAsync();
            var api = await this.GetAllApiResources().ToArrayAsync();
            return new Resources(identity, api);
        }

        private IQueryable<IdentityResource> GetAllIdentityResources()
        {
            var identity = this.dbContext.Set<Domain.Entity.IdentityResource>()
                                    .AsModel();

            return identity;
        }

        private IQueryable<ApiResource> GetAllApiResources()
        {
            var api = this.dbContext.Set<Domain.Entity.ApiResource>()
                                    .AsModel();

            return api;
        }
    }
}