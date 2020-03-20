using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups");
            builder.HasKey(u => u.Id);

            builder.HasIndex(r => new { r.ApiResourceId,  r.NormalizedName }).IsUnique().HasName("NormalizedNameIndex");
            
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.Description).HasMaxLength(256);
            
            builder.HasOne(u => u.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(uc => uc.ParentId).IsRequired(false);

            builder.HasMany(u => u.Roles)
                .WithOne(x => x.Group)
                .HasForeignKey(uc => uc.GroupId).IsRequired();

            builder.HasMany(u => u.Users)
                .WithOne(x => x.Group)
                .HasForeignKey(uc => uc.GroupId).IsRequired();

            builder.HasOne(x => x.ApiResource).WithMany(c=>c.Groups).HasForeignKey(f => f.ApiResourceId).IsRequired(false);
        }
    }
}