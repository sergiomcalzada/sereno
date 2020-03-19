﻿namespace Sereno.Domain.Entity
{
    public class ClientClaim
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}