using basic_ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace basic_ecommerce.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
    }
}
