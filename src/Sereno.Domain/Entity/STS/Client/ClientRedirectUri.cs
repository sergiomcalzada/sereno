﻿namespace Sereno.Domain.Entity
{
    public class ClientRedirectUri
    {
        public int Id { get; set; }
        public string RedirectUri { get; set; }
        public Client Client { get; set; }
        public int ClientId { get; set; }
    }
}