namespace Sereno.Domain.Entity
{
    public class ClientSecret : Secret
    {
        public Client Client { get; set; }
        public string ClientId { get; set; }
    }
}