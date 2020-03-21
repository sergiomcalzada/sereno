using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Client
{
    public class ClientSecretConfiguration : IEntityTypeConfiguration<ClientSecret>
    {
        public void Configure(EntityTypeBuilder<ClientSecret> builder)
        {
            builder.ToTable("ClientSecret");
            builder.Property(x => x.Value).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.Type).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(2000).IsRequired();

            builder.HasOne(x => x.Client).WithMany(x => x.ClientSecrets).HasForeignKey(x => x.ClientId).IsRequired();
        }
    }
}