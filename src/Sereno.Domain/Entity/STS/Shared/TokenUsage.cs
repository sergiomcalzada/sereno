namespace  Sereno.Domain.Entity
{
    public enum TokenUsage
    {
        /// <summary>Re-use the refresh token handle</summary>
        ReUse,
        /// <summary>Issue a new refresh token handle every time</summary>
        OneTimeOnly,
    }
}