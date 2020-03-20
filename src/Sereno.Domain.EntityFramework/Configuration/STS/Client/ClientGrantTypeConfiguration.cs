using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Client
{
    public class ClientGrantTypeConfiguration : IEntityTypeConfiguration<ClientGrantType>
    {
        public void Configure(EntityTypeBuilder<ClientGrantType> builder)
        {
            builder.ToTable("ClientGrantType");
            builder.Property(x => x.GrantType).HasMaxLength(2000).IsRequired();

            builder.HasOne(x => x.Client).WithMany(x => x.AllowedGrantTypes).HasForeignKey(x => x.ClientId).IsRequired();
        }
    }
}