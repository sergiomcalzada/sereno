using System;
using System.Collections.Generic;

namespace Sereno.Domain.Entity
{
    public class ApiResource
    {
        public int Id { get; set; }
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<ApiResourceSecurityAlgorithm> AllowedAccessTokenSigningAlgorithms { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public List<ApiResourceSecret> Secrets { get; set; }
        public List<ApiResourceScope> Scopes { get; set; }
        public List<ApiResourceClaim> UserClaims { get; set; }
        public List<ApiResourceProperty> Properties { get; set; }
        
        public DateTime? LastAccessed { get; set; }
        public bool NonEditable { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }

    public class ApiResourceSecurityAlgorithm
    {
        public int Id { get; set; }
        public SecurityAlgorithm SecurityAlgorithm { get; set; }
        public ApiResource ApiResource { get; set; }
        public int ApiResourceId { get; set; }
    }
}
