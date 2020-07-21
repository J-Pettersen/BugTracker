using System;
using System.ComponentModel.DataAnnotations;

namespace DTO
{
    public class User
    {

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public virtual String Name { get; set; }

        [Required]
        [StringLength(256)]
        [Display(Name = "Email Address")]
        public virtual String EmailAddress { get; set; }

        [StringLength(50)]
        public String Role { get; set; }

    }
}
