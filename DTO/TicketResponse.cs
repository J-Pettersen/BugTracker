namespace DTO
{
    public class TicketResponse : Ticket
    {

        public User AssignedDev { get; set; }

        public User SubmittedBy { get; set; }

        public Project Project { get; set; }

    }
}
