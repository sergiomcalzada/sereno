namespace Sereno.Domain.Entity
{
    public class ApiScopeClaim : BaseClaim
    {
        public int ApiScopeId { get; set; }
        public ApiScope ApiScope { get; set; }
    }
}