using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration
{
    public class UserClaimConfiguration : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> builder)
        {
            builder.ToTable("UserClaims");
            builder.HasKey(uc => uc.Id);
        }
    }
}