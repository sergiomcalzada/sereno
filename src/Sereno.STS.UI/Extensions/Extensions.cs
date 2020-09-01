using System;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;

namespace Sereno.STS.UI.Extensions
{
    public static class AuthorizationRequestExtensions
    {
        /// <summary>
        /// Checks if the redirect URI is for a native client.
        /// </summary>
        /// <returns></returns>
        public static bool IsNativeClient(this AuthorizationRequest context)
        {
            return !context.RedirectUri.StartsWith("https", StringComparison.Ordinal)
               && !context.RedirectUri.StartsWith("http", StringComparison.Ordinal);
        }

    }
    
}
