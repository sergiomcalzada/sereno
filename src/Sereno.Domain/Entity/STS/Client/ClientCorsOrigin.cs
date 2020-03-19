namespace Sereno.Domain.Entity
{
    public class ClientCorsOrigin
    {
        public int Id { get; set; }
        public string Origin { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}