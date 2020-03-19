namespace Sereno.Domain.Entity
{
    public class UserGroup 
    {
        /// <summary>
        /// Gets or sets the primary key of the user that is linked to a group.
        /// </summary>
        public virtual string UserId { get; set; }

        public virtual User User { get; set; }

        /// <summary>
        /// Gets or sets the primary key of the group that is linked to the user.
        /// </summary>
        public virtual string GroupId { get; set; }

        public virtual Group Group { get; set; }

        
    }
}