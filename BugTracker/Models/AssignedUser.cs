namespace BugTracker.Models
{
    public class AssignedUser
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

    }
}
