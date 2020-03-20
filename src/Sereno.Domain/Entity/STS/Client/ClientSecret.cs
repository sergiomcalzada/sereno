namespace Sereno.Domain.Entity
{
    public class ClientSecret : Secret
    {
        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}