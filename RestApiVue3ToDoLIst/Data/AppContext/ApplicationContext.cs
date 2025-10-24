namespace RestApiVue3ToDoLIst.Data.AppContext;

using Microsoft.EntityFrameworkCore;
using RestApiVue3ToDoLIst.Data.Models.Entities;

public class ApplicationContext : DbContext
{
    public DbSet<Job> Jobs { get; set; } = null!;
    public DbSet<Status> Statuses { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        Database.EnsureCreated();  
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
                new User { 
                    Id = 1,
                    Login = "Admin",
                    Name = "Admin",
                    SurName = "_",
                    LastName = "_",       
                    Password = "Admin",
                    Position = "boss"
                }
        );

        modelBuilder.Entity<Status>().HasData(
                [
                    new Status { Id = 1, StatusCode = 1, StatusName = "ToDo" },
                    new Status { Id = 2, StatusCode = 2, StatusName = "InProgress" },
                    new Status { Id = 3, StatusCode = 3, StatusName = "Done" }                    
                ]            
        );
    }
}
