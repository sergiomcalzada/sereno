using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Sereno.Domain.Entity;
using Sereno.Domain.EntityFramework;

namespace Sereno.STS.AspNetIdentity
{
    public class UserStore : UserStore<User>
    {
        public UserStore(DataContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}