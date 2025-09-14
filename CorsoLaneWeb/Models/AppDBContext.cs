using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CorsoLaneWeb.Models
{
    public class AppDBContext(IConfiguration configuration) : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection")); // Add your connection string here
        }
        
        DbSet<Order> Orders { get; set; }

        DbSet<products_entity> products { get; set; }


        DbSet<Category> Categories { get; set; }
       
        DbSet<user> Users { get; set; } 
        
        DbSet<SubCategory> SubCategories { get; set; }

        DbSet<OrderItem> OrderItems { get; set; }



        



    }
}
