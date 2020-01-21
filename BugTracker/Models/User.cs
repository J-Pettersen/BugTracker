using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public String Name { get; set; }

        [Required]
        [StringLength(256)]
        public String EmailAddress { get; set; }

        [StringLength(200)]
        public String Role { get; set; }

        public Project ProjectManaged { get; set; }
        public List<Ticket> Tickets { get; set;}

        public List<AssignedUser> AssignedUsers { get; set; }
    }
}
