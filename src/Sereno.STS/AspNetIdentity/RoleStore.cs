using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Sereno.Domain.Entity;
using Sereno.Domain.EntityFramework;

namespace Sereno.STS.AspNetIdentity
{
    public class RoleStore : RoleStore<Role, DataContext, string, UserRole, RoleClaim>
    {
        public RoleStore(DataContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}