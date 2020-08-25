using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Client
{
    public class ClientIdPRestrictionConfiguration : IEntityTypeConfiguration<ClientIdPRestriction>
    {
        public void Configure(EntityTypeBuilder<ClientIdPRestriction> builder)
        {
            builder.ToTable("ClientIdPRestriction");
            
            builder.HasOne(x => x.Client).WithMany(x => x.IdentityProviderRestrictions).HasForeignKey(x => x.ClientId).IsRequired();
            builder.HasOne(x => x.IdentityProvider).WithMany(x => x.IdentityProviderRestrictions).HasForeignKey(x => x.ClientId).IsRequired();

        }


    }
}