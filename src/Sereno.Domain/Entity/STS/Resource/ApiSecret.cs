namespace Sereno.Domain.Entity
{
    public class ApiSecret : Secret
    {
        public ApiResource ApiResource { get; set; }
        public int ApiResourceId { get; set; }
    }
}