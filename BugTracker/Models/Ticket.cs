using System;
using System.ComponentModel.DataAnnotations;

namespace BugTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public String Title { get; set; }

        [Required]
        [StringLength(4000)]
        public String Description { get; set; }
        
        [Required]
        [StringLength(200)]
        public String Priority { get; set; }

        [Required]
        [StringLength(200)]
        public String Status { get; set; } = "open";

        [Required]
        [StringLength(200)]
        public String TicketType { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        public int AssignedDevId { get; set; }
        public User AssignedDev { get; set; }

        [Required]
        public int SubmittedById { get; set; }
        public User SubmittedBy { get; set; }

        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
