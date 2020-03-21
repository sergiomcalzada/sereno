using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Client
{
    public class ClientScopeConfiguration : IEntityTypeConfiguration<ClientScope>
    {
        public void Configure(EntityTypeBuilder<ClientScope> builder)
        {
            builder.ToTable("ClientScope");
            builder.Property(x => x.Scope).HasMaxLength(2000).IsRequired();

            builder.HasOne(x => x.Client).WithMany(x => x.AllowedScopes).HasForeignKey(x => x.ClientId).IsRequired();
        }
    }
}