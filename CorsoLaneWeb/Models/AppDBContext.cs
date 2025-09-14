// Add these using statements at the top
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Models
{
    // 1. Inherit from IdentityDbContext<user>
    // 2. Use the correct constructor
    public class AppDBContext : IdentityDbContext<user>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        // Your custom DbSets go here
        public DbSet<Order> Orders { get; set; }
        public DbSet<products_entity> products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // REMOVED: The OnConfiguring method is no longer needed here.
        // REMOVED: The DbSet<user> is inherited from IdentityDbContext.
    }
}