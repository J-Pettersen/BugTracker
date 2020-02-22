using System;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data
{
    public class Ticket : DTO.Ticket
    {

        public virtual User AssignedDev { get; set; }        
        public virtual User SubmittedBy { get; set; }
        public virtual Project Project { get; set; }

    }
}
