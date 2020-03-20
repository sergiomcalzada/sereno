namespace Sereno.Domain.Entity
{
    public class ClientPostLogoutRedirectUri
    {
        public string Id { get; set; }
        public string Uri { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}