using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class User
    {

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public String Name { get; set; }

        [Required]
        [StringLength(256)]

        [Display(Name = "Email Address")]
        public String EmailAddress { get; set; }

        [StringLength(50)]
        public String Role { get; set; }

    }
}
