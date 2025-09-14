using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CorsoLaneWeb.Models
{
    public class AppDBContext(IConfiguration configuration) : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("")); // Add your connection string here
        }
        
        DbSet<Order> Orders { get; set; }
        DbSet<Categories> Categories { get; set; }
       
        DbSet<user> Users { get; set; } 

        



    }
}
