using Microsoft.EntityFrameworkCore;
using API_Neeew.Models;

namespace API_Neeew.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}