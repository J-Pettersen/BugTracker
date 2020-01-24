using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data
{
    public class User : DTO.User
    {

        public virtual ICollection<UserProject> UsersProjects { get; set; }

        public virtual ICollection<Project> ProjectsManaged { get; set; }

        public virtual ICollection<Ticket> AssignedTickets { get; set; }

        public virtual ICollection<Ticket> SubmittedTickets { get; set; }

    }
}
