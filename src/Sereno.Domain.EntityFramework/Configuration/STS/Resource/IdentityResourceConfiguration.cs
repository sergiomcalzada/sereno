using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Resource
{
    public class IdentityResourceConfiguration : IEntityTypeConfiguration<IdentityResource>
    {
        public void Configure(EntityTypeBuilder<IdentityResource> builder)
        {
            builder.ToTable("IdentityResource").HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.DisplayName).HasMaxLength(2000);
            builder.Property(x => x.Description).HasMaxLength(4000);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.UserClaims).WithOne(x => x.IdentityResource).HasForeignKey(x => x.IdentityResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}