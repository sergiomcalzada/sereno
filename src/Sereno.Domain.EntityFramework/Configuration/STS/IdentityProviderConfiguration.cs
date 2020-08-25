using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS
{
    public class IdentityProviderConfiguration : IEntityTypeConfiguration<IdentityProvider>
    {
        public void Configure(EntityTypeBuilder<IdentityProvider> builder)
        {
            builder.ToTable("IdentityProvider");
            builder.Property(x => x.Name).HasMaxLength(2000).IsRequired();

        }


    }
}