using System.Collections.Generic;

namespace Sereno.Domain.Entity
{
    public class IdentityProvider
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ClientIdPRestriction> IdentityProviderRestrictions { get; set; }
    }
}