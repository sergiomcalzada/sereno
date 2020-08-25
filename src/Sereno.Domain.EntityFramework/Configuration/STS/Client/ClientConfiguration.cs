using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Client
{
    public class ClientConfiguration : IEntityTypeConfiguration<Entity.Client>
    {
        public void Configure(EntityTypeBuilder<Entity.Client> builder)
        {

            builder.ToTable("Client");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.ClientId).IsUnique();

            builder.Property(x => x.ClientId).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.ProtocolType).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.ClientName).HasMaxLength(2000);
            builder.Property(x => x.ClientUri).HasMaxLength(20000);

            builder.HasMany(x => x.AllowedGrantTypes).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.RedirectUris).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.PostLogoutRedirectUris).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.AllowedScopes).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.ClientSecrets).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Claims).WithOne(x => x.Client).IsRequired().HasForeignKey(x => x.ClientId).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.IdentityProviderRestrictions).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.AllowedCorsOrigins).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.AllowedIdentityTokenSigningAlgorithms).WithOne(x => x.Client).HasForeignKey(x => x.ClientId).IsRequired().OnDelete(DeleteBehavior.Cascade);

        }
    }
}