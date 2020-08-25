using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Client
{
    public class ClientPostLogoutRedirectUriConfiguration : IEntityTypeConfiguration<ClientPostLogoutRedirectUri>
    {
        public void Configure(EntityTypeBuilder<ClientPostLogoutRedirectUri> builder)
        {
            builder.ToTable("ClientPostLogoutRedirectUri");
            builder.Property(x => x.PostLogoutRedirectUri).HasMaxLength(2000).IsRequired();

            builder.HasOne(x => x.Client).WithMany(x => x.PostLogoutRedirectUris).HasForeignKey(x => x.ClientId).IsRequired();
        }
    }
}