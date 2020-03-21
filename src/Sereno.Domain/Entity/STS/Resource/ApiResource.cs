using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sereno.Domain.Entity
{
    public class ApiResource
    {
        public string Id { get; set; }
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public ICollection<ApiSecret> Secrets { get; set; } = new Collection<ApiSecret>();
        public ICollection<ApiScope> Scopes { get; set; } = new Collection<ApiScope>();
        public ICollection<ApiResourceClaim> UserClaims { get; set; } = new Collection<ApiResourceClaim>();

        public virtual ICollection<Role> Roles { get; } = new Collection<Role>();
        public virtual ICollection<Group> Groups { get; } = new Collection<Group>();
    }
}
