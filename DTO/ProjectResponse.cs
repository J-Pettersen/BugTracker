using System.Collections.Generic;

namespace DTO
{
    public class ProjectResponse : Project
    {
        public virtual User ProjectManager { get; set; }
        public virtual ICollection<User> AssignedUsers { get; set; } = new List<User>();
    }
}
