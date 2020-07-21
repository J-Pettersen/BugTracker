using System.Collections.Generic;

namespace BackEnd.Data
{
    public class Project  : DTO.Project
    {

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<UserProject> UsersProjects { get; set; } = new List<UserProject>();

        public virtual User ProjectManager { get; set; }

    }
}
