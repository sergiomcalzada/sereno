namespace Sereno.Domain.Entity
{
    public class IdentityResourceClaim : BaseClaim
    {
        public int IdentityResourceId { get; set; }
        public IdentityResource IdentityResource { get; set; }
    }
}