namespace Sereno.Domain.Entity
{
    public class ClientClaim
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public Client Client { get; set; }
        public string ClientId { get; set; }
    }
}