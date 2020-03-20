using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Resource
{
    public class ApiScopeConfiguration : IEntityTypeConfiguration<ApiScope>
    {
        public void Configure(EntityTypeBuilder<ApiScope> builder)
        {
            builder.ToTable("ApiScope").HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.DisplayName).HasMaxLength(2000);
            builder.Property(x => x.Description).HasMaxLength(4000);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.UserClaims).WithOne(x => x.ApiScope).HasForeignKey(x => x.ApiScopeId).IsRequired().OnDelete(DeleteBehavior.Cascade);
        }
    }
}