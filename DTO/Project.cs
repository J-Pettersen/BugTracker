using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO
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
        [Display(Name="Project Manager")]
        public int ProjectManagerId { get; set; }
               
    }
}
