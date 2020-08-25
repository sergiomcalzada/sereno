namespace Sereno.Domain.Entity
{
    public class ClientApiScope
    {
       
        public int ApiScopeId { get; set; }
        public ApiScope ApiScope { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}