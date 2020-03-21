using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sereno.Domain.Entity;

namespace Sereno.STS.AspNetIdentity
{
    public class UserManager : UserManager<User>
    {
        public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger) : base(
            store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,
            logger)
        {
        }

        public override Task<IdentityResult> CreateAsync(User user)
        {
            //TODO: replace Id generation with snowflake or similar
            user.Id = Guid.NewGuid().ToString().Replace("-", "");
            return base.CreateAsync(user);
        }

        public override Task<IdentityResult> CreateAsync(User user, string password)
        {
            //TODO: replace Id generation with snowflake or similar
            user.Id = Guid.NewGuid().ToString().Replace("-", "");
            return base.CreateAsync(user, password);
        }
    }
}