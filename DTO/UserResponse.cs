using System.Collections.Generic;

namespace DTO
{
    public class UserResponse : User
    {
        public virtual ICollection<Project> UsersProjects { get; set; } = new List<Project>();
    }
}
