using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Sereno.Domain.Entity
{
    public class Group 
    {
        /// <summary>
        ///     Identifier of the Group
        /// </summary>
        public virtual string Id { get; set; }

        /// <summary>
        ///     Name of the group
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        ///     Normalized name (uppercase by default) of the group
        /// </summary>
        public virtual string NormalizedName { get; set; }

        /// <summary>
        ///     Description of the group
        /// </summary>
        public virtual string Description { get; set; }

        public virtual string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        ///     Identifier of the parent group
        /// </summary>
        public virtual string ParentId { get; set; }
        public virtual Group Parent { get; set; }

        /// <summary>
        ///     Identifier of the associated ApiResource
        /// </summary>
        public virtual string ApiResourceId { get; set; }
        public virtual ApiResource ApiResource { get; set; }

        
        public virtual ICollection<Group> Children { get; } = new Collection<Group>();

        /// <summary>
        ///     Navigation property for the Users this group have assigned.
        /// </summary>
        public virtual ICollection<UserGroup> Users { get; } = new Collection<UserGroup>();

        /// <summary>
        ///     Navigation property for the Roles this group have assigned.
        /// </summary>
        public virtual ICollection<GroupRole> Roles { get; } = new Collection<GroupRole>();

    }
}