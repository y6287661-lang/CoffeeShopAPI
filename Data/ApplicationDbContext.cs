
using API_Neeew.Data;
using API_Neeew.Models;
using Microsoft.EntityFrameworkCore;

namespace API_Neeew.Data
{
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {


    }               

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(new User
        {
            Id = 1,
            Name = "Test User",
            Email = "test@example.com",
            Password = "123456"
        });
    }
}


}
