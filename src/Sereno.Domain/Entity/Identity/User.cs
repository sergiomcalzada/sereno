using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Sereno.Domain.Entity
{
    public class User : IdentityUser<string>
    {
        
        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public virtual ICollection<UserRole> Roles { get; } = new Collection<UserRole>();

        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<UserClaim> Claims { get; } = new Collection<UserClaim>();

        /// <summary>
        /// Navigation property for this users login accounts.
        /// </summary>
        public virtual ICollection<UserLogin> Logins { get; } = new Collection<UserLogin>();

        /// <summary>
        /// Navigation property for this user assigned groups
        /// </summary>
        public virtual ICollection<UserGroup> Groups { get; } = new Collection<UserGroup>();


    }
   
}