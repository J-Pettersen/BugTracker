﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class Project
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public String Title { get; set; }

        [Required]
        [StringLength(4000)]
        public String Description { get; set; }         
                
        [Display(Name="Project Manager")]
        public int? ProjectManagerId { get; set; }
               
    }
}
