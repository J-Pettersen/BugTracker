using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class BTContext : DbContext
    {
        public BTContext(DbContextOptions<BTContext> options)
            : base(options)
        {            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<User>()
                .HasOne(s => s.ProjectManaged)
                .WithOne(p => p.ProjectManager)
                .HasForeignKey<Project>(s => s.ProjectManagerId)
                .HasConstraintName("FK_Project_Manager");

            modelbuilder.Entity<Ticket>()
                .HasOne(a => a.AssignedDev)
                .WithMany(t => t.Tickets)
                .HasForeignKey(a => a.AssignedDevId)
                .HasConstraintName("FK_Assigned_Dev");

            modelbuilder.Entity<Ticket>()
                .HasOne(s => s.SubmittedBy)
                .WithMany(t => t.Tickets)
                .HasForeignKey(s => s.SubmittedById)
                .HasConstraintName("FK_Submitted_By");

            modelbuilder.Entity<Ticket>()
                .HasOne(p => p.Project)
                .WithMany(t => t.Tickets)
                .HasForeignKey(p => p.ProjectId)
                .HasConstraintName("FK_Project");

            modelbuilder.Entity<AssignedUser>()
                .HasOne(u => u.User)
                .WithMany(p => p.AssignedUsers)
                .HasForeignKey(u => u.UserId);

            modelbuilder.Entity<AssignedUser>()
                .HasOne(p => p.Project)
                .WithMany(u => u.AssignedUsers)
                .HasForeignKey(p => p.ProjectId);
        }
    }
}
