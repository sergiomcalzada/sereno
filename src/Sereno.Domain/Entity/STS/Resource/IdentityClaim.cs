namespace Sereno.Domain.Entity
{
    public class IdentityClaim : BaseClaim
    {
        public IdentityResource IdentityResource { get; set; }
        public int IdentityResourceId { get; set; }
    }
}