namespace  Sereno.Domain.Entity
{
    public enum AccessTokenType
    {
        /// <summary>Self-contained Json Web Token</summary>
        Jwt,
        /// <summary>Reference token</summary>
        Reference,
    }
}