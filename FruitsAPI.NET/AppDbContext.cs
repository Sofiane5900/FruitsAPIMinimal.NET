using Microsoft.EntityFrameworkCore;

namespace FruitsAPI.NET
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Fruits> Fruits { get; set; }
    }
}
