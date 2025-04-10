using Microsoft.EntityFrameworkCore;
using Projector.Models.Entities;

namespace Projector.Data
{
    public class ProjectorDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }  
        public ProjectorDbContext(DbContextOptions<ProjectorDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(e => e.Persons)
                .WithMany(e => e.Projects)
                .UsingEntity("ProjectAssignments");
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Username = "admin@test.com", Password = "admin12345"}
            );
        }
    }
}
