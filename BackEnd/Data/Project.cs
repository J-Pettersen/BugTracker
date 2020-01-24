using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data
{
    public class Project  : DTO.Project
    {

        public virtual ICollection<Ticket> Tickets { get; set; }

        public virtual ICollection<UserProject> UsersProjects { get; set; }

        public User ProjectManager { get; set; }

    }
}
