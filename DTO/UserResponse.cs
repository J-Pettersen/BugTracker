using System.Collections.Generic;

namespace DTO
{
    public class UserResponse : User
    {
        public ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
