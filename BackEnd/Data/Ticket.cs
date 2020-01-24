using System;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Data
{
    public class Ticket : DTO.Ticket
    {

        public User AssignedDev { get; set; }        
        public User SubmittedBy { get; set; }
        public Project Project { get; set; }

    }
}
