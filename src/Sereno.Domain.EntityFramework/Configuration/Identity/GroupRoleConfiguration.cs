using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration
{
    public class GroupRoleConfiguration : IEntityTypeConfiguration<GroupRole>
    {
        public void Configure(EntityTypeBuilder<GroupRole> builder)
        {
            builder.ToTable("GroupRoles");
            builder.HasKey(l => new
            {
                l.GroupId,
                l.RoleId
            });
        }
    }
}
