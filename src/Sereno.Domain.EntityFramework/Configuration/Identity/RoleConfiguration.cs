using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration
{
  public class RoleConfiguration : IEntityTypeConfiguration<Role>
  {
    public void Configure(EntityTypeBuilder<Role> builder)
    {
      builder.HasKey(r => r.Id);
      builder.HasIndex(r => new { r.ApiResourceId, r.NormalizedName }).IsUnique().HasName("NormalizedNameIndex");

      builder.ToTable("Roles");
      builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();
      builder.Property(r => r.Name).HasMaxLength(256);
      builder.Property(r => r.NormalizedName).HasMaxLength(256);

      builder.HasMany(r => r.Users).WithOne(x => x.Role).HasForeignKey(ur => ur.RoleId).IsRequired();
      builder.HasMany(r => r.Claims).WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

      builder.HasOne(x => x.ApiResource).WithMany(c => c.Roles).HasForeignKey(f => f.ApiResourceId).IsRequired(false);

      //Fix index definitions
      var indexes = builder.Metadata.GetIndexes();
      foreach (var index in indexes)
      {
        var anotation = index.FindAnnotation(RelationalAnnotationNames.Name);
        if (anotation != null &&
            anotation.Value is string value && value == "RoleNameIndex")
        {
          index.IsUnique = false;
          //or
          //builder.Metadata.RemoveIndex(index.Properties);
        }
      }
    }
  }
}