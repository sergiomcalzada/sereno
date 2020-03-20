using System.Collections.Generic;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Stores;
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

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiResource> FindApiResourceAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}