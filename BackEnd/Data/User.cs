using System.Collections.Generic;

namespace BackEnd.Data
{
    public class User : DTO.User
    {

        public virtual ICollection<UserProject> UsersProjects { get; set; } = new List<UserProject>();

        public virtual ICollection<Project> ProjectsManaged { get; set; }

        public virtual ICollection<Ticket> AssignedTickets { get; set; }

        public virtual ICollection<Ticket> SubmittedTickets { get; set; }

    }
}
