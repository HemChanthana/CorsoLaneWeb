using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoLaneWeb.Models
{
    public class products_entity
    {
        [Key]
        public int Id { get; set; } // Primary Key

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }




        [Required]
        public decimal Price { get; set; } // Use 'decimal' for currency to avoid rounding errors



        [NotMapped]
        [Display(Name = "Product Image")]
        public IFormFile ImageUrl { get; set; }

        public int StockQuantity { get; set; }


        // --- Relationship to SubCategory ---

        // 1. Foreign Key property
        // This holds the ID of the SubCategory this product belongs to.
        public int SubCategoryId { get; set; }

        // 2. Navigation property
        // This allows you to access the related SubCategory object from a Product.
        // For example: myProduct.SubCategory.Name
        [ForeignKey("SubCategoryId")]
        public SubCategory SubCategory { get; set; }
    }
}
