
using System.Net;

namespace CorsoLaneWeb.Models
{
    public class user
    {
        public int UserId { get; set; }           // PK
        public string Name { get; set; }
        public string Email { get; set; }         // Unique
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; }          // "Customer" or "Admin"

        // Navigation properties
        public ICollection<Order> Orders { get; set; }


    }
}
