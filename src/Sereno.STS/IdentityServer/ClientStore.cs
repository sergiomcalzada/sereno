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
                .AsModel()
                .FirstOrDefaultAsync();
            return client;
        }
    }
}