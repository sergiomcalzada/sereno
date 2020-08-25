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
        private readonly IIdGenerator idGenerator;

        public UserManager(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger, IIdGenerator idGenerator) : base(
            store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services,
            logger)
        {
            this.idGenerator = idGenerator;
        }

        public override Task<IdentityResult> CreateAsync(User user)
        {
            user.Id = this.idGenerator.Next();
            return base.CreateAsync(user);
        }

        public override Task<IdentityResult> CreateAsync(User user, string password)
        {
            user.Id = this.idGenerator.Next();
            return base.CreateAsync(user, password);
        }
    }

    //TODO: implement
    public interface IIdGenerator
    {
        int Next();
    }

    public class IdGenerator : IIdGenerator
    {
        private int current;
        public int Next()
        {
            return ++this.current;
        }
    }
}