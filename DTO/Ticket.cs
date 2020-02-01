using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public String Title { get; set; }
        
        [StringLength(4000)]
        public String Description { get; set; }
        
        [Required]
        [StringLength(10)]
        public String Priority { get; set; }

        [Required]
        [StringLength(10)]
        public String Status { get; set; } = "open";

        [Required]
        [StringLength(50)]
        [Display(Name ="Ticket Type")]
        public String TicketType { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name ="Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name ="Assigned Developer")]
        public int? AssignedDevId { get; set; }

        [Display(Name ="Submitted By")]
        public int? SubmittedById { get; set; }

        [Display(Name ="Project")]
        public int? ProjectId { get; set; }

    }
}
