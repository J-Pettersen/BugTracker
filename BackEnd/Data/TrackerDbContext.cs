using Microsoft.EntityFrameworkCore;

namespace BackEnd.Data
{
    public class TrackerDBContext : DbContext
    {
        public TrackerDBContext(DbContextOptions<TrackerDBContext> options)
            : base(options)
        {            
        }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
           
            modelbuilder.Entity<User>()
                .HasIndex(u => u.EmailAddress)
                .IsUnique();     

            modelbuilder.Entity<UserProject>()
                .HasKey(up => new { up.UserId, up.ProjectId });
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
    }
}
