using System;

namespace  Sereno.Domain.Entity
{
    public abstract class Secret
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public DateTime? Expiration { get; set; }
        public string Type { get; set; } = SecretTypes.SharedSecret;
    }
}