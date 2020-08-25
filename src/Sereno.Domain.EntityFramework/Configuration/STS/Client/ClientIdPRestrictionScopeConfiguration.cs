using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS
{
    public class ClientIdPRestrictionScopeConfiguration : IEntityTypeConfiguration<Entity.ClientIdPRestriction>
    {
        public void Configure(EntityTypeBuilder<ClientIdPRestriction> builder)
        {
            builder.ToTable("Client");
            builder.HasKey(x => new {x.ClientId, x.IdentityProviderId});
        }
    }
}