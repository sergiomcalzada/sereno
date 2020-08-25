using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sereno.Domain.Entity;

namespace Sereno.Domain.EntityFramework.Configuration.STS.Resource
{
    public class ApiResourceScopeConfiguration : IEntityTypeConfiguration<ApiResourceScope>
    {
        public void Configure(EntityTypeBuilder<ApiResourceScope> builder)
        {
            builder.ToTable("ApiResourceScope").HasKey(x => new {x.ApiResourceId, x.ApiScopeId});
        }
    }
}