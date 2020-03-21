using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Resource
{
    public class ApiResourceConfiguration : IEntityTypeConfiguration<ApiResource>
    {
        public void Configure(EntityTypeBuilder<ApiResource> builder)
        {
            builder.ToTable("ApiResource").HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.DisplayName).HasMaxLength(2000);
            builder.Property(x => x.Description).HasMaxLength(4000);

            builder.HasIndex(x => x.Name).IsUnique();

            builder.HasMany(x => x.Secrets).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Scopes).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.UserClaims).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            //Relaciones con Roles y grupos (descendientes directos)
            builder.HasMany(x => x.Roles).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId).IsRequired(false);//.OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.Groups).WithOne(x => x.ApiResource).HasForeignKey(x => x.ApiResourceId).IsRequired(false);//.OnDelete(DeleteBehavior.Cascade);
        }
    }
}
