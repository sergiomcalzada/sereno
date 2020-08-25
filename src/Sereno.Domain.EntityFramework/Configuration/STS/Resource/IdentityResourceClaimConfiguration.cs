using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Resource
{
    public class IdentityResourceClaimConfiguration : IEntityTypeConfiguration<IdentityResourceClaim>
    {
        public void Configure(EntityTypeBuilder<IdentityResourceClaim> builder)
        {
            builder.ToTable("IdentityResourceClaim").HasKey(x => x.Id);

            builder.Property(x => x.Type).HasMaxLength(2000).IsRequired();
        }
    }
}