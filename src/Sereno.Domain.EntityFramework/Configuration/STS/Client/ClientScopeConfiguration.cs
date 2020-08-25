using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Client
{
    public class ClientScopeConfiguration : IEntityTypeConfiguration<ClientApiScope>
    {
        public void Configure(EntityTypeBuilder<ClientApiScope> builder)
        {
            builder.ToTable("ClientApiScope");

            builder.HasOne(x => x.Client).WithMany(x => x.AllowedScopes).HasForeignKey(x => x.ClientId).IsRequired();
            builder.HasOne(x => x.ApiScope).WithMany(x => x.AllowedClients).HasForeignKey(x => x.ApiScopeId).IsRequired();
        }
    }
}