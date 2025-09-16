// Add these using statements at the top
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Models
{

    public class AppDBContext : IdentityDbContext<user>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }


        public DbSet<Order> Orders { get; set; }
        public DbSet<products_entity> products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

      
    }
}