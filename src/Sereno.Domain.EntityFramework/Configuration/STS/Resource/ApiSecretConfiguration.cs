using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Resource
{
    public class ApiSecretConfiguration : IEntityTypeConfiguration<ApiResourceSecret>
    {
        public void Configure(EntityTypeBuilder<ApiResourceSecret> builder)
        {
            builder.ToTable("ApiResourceSecret").HasKey(x => x.Id);

            builder.Property(x => x.Description).HasMaxLength(4000);
            builder.Property(x => x.Value).HasMaxLength(2000);
            builder.Property(x => x.Type).HasMaxLength(2000);
        }
    }
}