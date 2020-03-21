using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Client
{
    public class ClientClaimConfiguration : IEntityTypeConfiguration<ClientClaim>
    {
        public void Configure(EntityTypeBuilder<ClientClaim> builder)
        {
            builder.ToTable("ClientClaim");
            builder.Property(x => x.Type).HasMaxLength(20000).IsRequired();
            builder.Property(x => x.Value).HasMaxLength(20000).IsRequired();

            builder.HasOne(x => x.Client).WithMany(x => x.Claims).HasForeignKey(x => x.ClientId).IsRequired();

        }
    }
}