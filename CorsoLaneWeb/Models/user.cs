
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace CorsoLaneWeb.Models
{
    public class user : IdentityUser<int>
    {
        // Add only your custom properties here
        public string user_fullname { get; set; }

        // The "Role" property is usually not stored directly here.
        // Roles are managed through Identity's role management system.
        // public string Role { get; set; } // It's better to remove this.

        // Navigation properties are fine
        public ICollection<Order> Orders { get; set; }

    }
}
