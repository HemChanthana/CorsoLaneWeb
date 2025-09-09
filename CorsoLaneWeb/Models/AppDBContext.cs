using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CorsoLaneWeb.Models
{
    public class AppDBContext(IConfiguration configuration) : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Data Source=DESKTOP-KN6CMME;Initial Catalog=CorsoLaneEC;Integrated Security=True;Trust Server Certificate=True")); // Add your connection string here
        }
        
    }
}
