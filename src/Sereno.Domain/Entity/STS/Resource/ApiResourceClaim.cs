﻿namespace Sereno.Domain.Entity
{
    public class ApiResourceClaim : BaseClaim
    {
        public ApiResource ApiResource { get; set; }
        public int ApiResourceId { get; set; }
    }
}