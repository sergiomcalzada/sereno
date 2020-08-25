namespace Sereno.Domain.Entity
{
    public class ApiResourceScope
    {
        public int ApiScopeId { get; set; }
        public ApiScope ApiScope { get; set; }

        public int ApiResourceId { get; set; }
        public ApiResource ApiResource { get; set; }
    }
}