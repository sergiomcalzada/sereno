using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sereno.Domain.Entity;

namespace Sereno.STS.AspNetIdentity
{
    public class SecurityStampValidator : SecurityStampValidator<User>
    {
        public SecurityStampValidator(IOptions<SecurityStampValidatorOptions> options,
            SignInManager<User> signInManager, ISystemClock clock, ILoggerFactory logger) : base(options, signInManager,
            clock, logger)
        {
        }
    }
}