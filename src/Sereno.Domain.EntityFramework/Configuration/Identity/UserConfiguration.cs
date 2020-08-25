using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.Id);

            builder.HasIndex(u => u.NormalizedUserName).HasName("NormalizedUserNameIndex").IsUnique();
            builder.HasIndex(u => u.NormalizedEmail).HasName("NormalizedEmailIndex");

            builder.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();
            builder.Property(u => u.UserName).HasMaxLength(256);
            builder.Property(u => u.NormalizedUserName).HasMaxLength(256);
            builder.Property(u => u.Email).HasMaxLength(256);
            builder.Property(u => u.NormalizedEmail).HasMaxLength(256);
            builder.HasMany(u => u.Claims).WithOne().HasForeignKey(uc => uc.UserId).IsRequired();
            builder.HasMany(u => u.Logins).WithOne().HasForeignKey(ul => ul.UserId).IsRequired();
            
            builder.HasMany(u => u.Roles).WithOne(u => u.User).HasForeignKey(ur => ur.UserId).IsRequired();
            
        }
    }
}