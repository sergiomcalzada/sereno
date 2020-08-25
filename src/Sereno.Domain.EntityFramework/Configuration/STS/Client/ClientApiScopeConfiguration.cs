using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS
{
    public class ClientApiScopeConfiguration : IEntityTypeConfiguration<Entity.ClientApiScope>
    {
        public void Configure(EntityTypeBuilder<ClientApiScope> builder)
        {
            builder.ToTable("Client");
            builder.HasKey(x => new {x.ClientId, x.ApiScopeId});
        }
    }
}