﻿using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sereno.Domain.Entity
{
    public class IdentityResource
    {
        public string Id { get; set; }
        public bool Enabled { get; set; } = true;
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool Required { get; set; }
        public bool Emphasize { get; set; }
        public bool ShowInDiscoveryDocument { get; set; } = true;
        public ICollection<IdentityClaim> UserClaims { get; set; } = new Collection<IdentityClaim>();
    }
}