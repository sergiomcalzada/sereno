using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Client
{
    public class ClientRedirectUriConfiguration : IEntityTypeConfiguration<ClientRedirectUri>
    {
        public void Configure(EntityTypeBuilder<ClientRedirectUri> builder)
        {
            builder.ToTable("ClientRedirectUri");
            builder.Property(x => x.RedirectUri).HasMaxLength(2000).IsRequired();

            builder.HasOne(x => x.Client).WithMany(x => x.RedirectUris).HasForeignKey(x => x.ClientId).IsRequired();
        }
    }
}