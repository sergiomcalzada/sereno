using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sereno.Domain.Entity
{
    public class ApiScope
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public ICollection<ApiScopeClaim> UserClaims { get; set; } = new Collection<ApiScopeClaim>();

        public ApiResource ApiResource { get; set; }
        public int ApiResourceId { get; set; }
    }
}