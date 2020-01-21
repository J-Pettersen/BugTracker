using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public String Name { get; set; }
        [Required]
        [StringLength(4000)]
        public String Description { get; set; }     
        public String Status { get; set; }
        
        [Required]
        public int ProjectManagerId { get; set; }
        public User ProjectManager  { get; set; }

        public List<Ticket> Tickets { get; set; }
        public List<AssignedUser> AssignedUsers { get; set; }
               
    }
}
