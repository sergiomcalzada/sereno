namespace Sereno.Domain.Entity
{
    public class ApiResourceClaim : BaseClaim
    {
        public int ApiResourceId { get; set; }
        public ApiResource ApiResource { get; set; }
    }
}