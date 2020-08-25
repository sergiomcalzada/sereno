namespace Sereno.Domain.Entity
{
    public class ClientIdPRestriction
    {
        public IdentityProvider  IdentityProvider { get; set; }
        public int IdentityProviderId { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}