namespace Sereno.Domain.Entity
{
    public class ApiScopeProperty : Property
    {
        public int ApiScopeId { get; set; }
        public ApiScope ApiScope { get; set; }
    }
}