namespace Sereno.Domain.Entity
{
    public class GroupRole 
    {
        //
        // Summary:
        //     Gets or sets the primary key of the group that is linked to a role.
        public virtual string GroupId { get; set; }
        public virtual Group Group { get; set; }

        //
        // Summary:
        //     Gets or sets the primary key of the role that is linked to the user.
        public virtual string RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
