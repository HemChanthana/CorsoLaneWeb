
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoLaneWeb.Models
{
    public class CartItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("ProductId")]

        public int ProductId { get; set; }


        public Product? Product { get; set; }

        public int Quantity { get; set; }
        public string? SessionId { get; set; } // For anonymous users
        public string? UserId { get; set; } // For logged-in users
    }
}