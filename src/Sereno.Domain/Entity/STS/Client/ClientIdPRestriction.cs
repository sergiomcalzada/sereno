﻿namespace Sereno.Domain.Entity
{
    public class ClientIdPRestriction
    {
        public int Id { get; set; }
        public string Provider { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}