using Microsoft.AspNetCore.Identity;
using System.Collections.Generic; 

namespace CorsoLaneWeb.Models
{
    public class user : IdentityUser
    {
        // Add *only* your custom properties here.
        // All other properties like Id, UserName, Email, etc.,
        // are inherited automatically from IdentityUser.
        public string user_fullname { get; set; }

        // Navigation properties are also fine to add.
        public ICollection<Order> Orders { get; set; }
    }
}