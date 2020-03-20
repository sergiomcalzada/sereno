using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Sereno.Domain.Entity
{
    public class Role : IdentityRole<string>
    {
        
        public virtual string ApiResourceId { get; set; }
        public virtual ApiResource ApiResource { get; set; }

        /// <summary>
        /// Navigation property for the Users this role possesses.
        /// </summary>
        public virtual ICollection<UserRole> Users { get; } = new Collection<UserRole>();

        /// <summary>
        /// Navigation property for the claims this role possesses.
        /// </summary>
        public virtual ICollection<RoleClaim> Claims { get; } = new Collection<RoleClaim>();

        /// <summary>
        /// Navigation property for the groups this role possesses.
        /// </summary>
        public virtual ICollection<GroupRole> Groups { get; } = new Collection<GroupRole>();

    }
}