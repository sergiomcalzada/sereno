namespace Sereno.Domain.Entity
{
    public class ClientScope
    {
        public string Id { get; set; }
        public string Scope { get; set; }
        public Client Client { get; set; }
        public string ClientId { get; set; }
    }
}