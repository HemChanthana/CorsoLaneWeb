using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoLaneWeb.Models
{
    public class Order
    {


        [Key]
        public int Id { get; set; } // Primary Key

        // --- User Relationship ---
        public int UserId { get; set; } // Foreign Key to the User table
        [ForeignKey("UserId")]
        public user User { get; set; }

        // --- Order Information ---
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }

        // --- Shipping Information ---
        [Required]
        public string Name { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        // Navigation property to the line items
        public ICollection<OrderItem> OrderItems { get; set; }




    }
}
