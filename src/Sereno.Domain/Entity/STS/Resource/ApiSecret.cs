namespace Sereno.Domain.Entity
{
    public class ApiSecret : Secret
    {
        public ApiResource ApiResource { get; set; }
        public string ApiResourceId { get; set; }
    }
}