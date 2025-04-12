using Microsoft.EntityFrameworkCore;
using Projector.Models.Entities;

namespace Projector.Data
{
    public class ProjectorDbContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Project> Projects { get; set; }
        public ProjectorDbContext(DbContextOptions<ProjectorDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Project>()
                .HasMany(e => e.Persons)
                .WithMany(e => e.Projects)
                .UsingEntity("ProjectAssignments");

            modelBuilder.Entity<Person>()
                .Property(e => e.UserName)
                .UseCollation("SQL_Latin1_General_CP1_CS_AS");  // CS = Case Sensitive

            modelBuilder.Entity<Person>().HasData(
                new Person { Id = 1, FirstName = "Admin", LastName = "Test", UserName = "admin@test.com", Password = "admin12345"}
            );
        }
    }
}
