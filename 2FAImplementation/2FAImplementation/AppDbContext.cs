using _2FAImplementation.Models;
using Microsoft.EntityFrameworkCore;

namespace _2FAImplementation
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        // Database context: User
        public DbSet<User> Users { get; set; }
    }
}
