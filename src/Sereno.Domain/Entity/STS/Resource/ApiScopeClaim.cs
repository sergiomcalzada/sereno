namespace Sereno.Domain.Entity
{
    public class ApiScopeClaim : BaseClaim
    {
        public ApiScope ApiScope { get; set; }
        public int ApiScopeId { get; set; }
    }
}