using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sereno.Domain.EntityFramework.Configuration.STS.PersistedGrant
{
    public class PersistedGrantConfiguration : IEntityTypeConfiguration<Entity.PersistedGrant>
    {
        public void Configure(EntityTypeBuilder<Entity.PersistedGrant> builder)
        {
            builder.ToTable("PersistedGrant");

            builder.Property(x => x.Key).HasMaxLength(2000).ValueGeneratedNever();
            builder.Property(x => x.Type).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.SubjectId).HasMaxLength(2000);
            builder.Property(x => x.ClientId).HasMaxLength(2000).IsRequired();
            builder.Property(x => x.CreationTime).IsRequired();
            // 50000 chosen to be explicit to allow enough size to avoid truncation, yet stay beneath the MySql row size limit of ~65K
            // apparently anything over 4K converts to nvarchar(max) on SqlServer
            builder.Property(x => x.Data).HasMaxLength(50000).IsRequired();

            builder.HasKey(x => x.Key);

            builder.HasIndex(x => new { x.SubjectId, x.ClientId, x.Type });
        }
    }
}
