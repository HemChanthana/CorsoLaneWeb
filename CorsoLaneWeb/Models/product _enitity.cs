using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoLaneWeb.Models
{
    public class products_entity
    {
        [Key]
        public int Id { get; set; }

        // --- Product Basic Info ---
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Size is required")]
        [StringLength(50)]
        public string Size { get; set; }

        [Required(ErrorMessage = "Color is required")]
        [StringLength(50)]
        public string Color { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be greater than 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock must be a non-negative number")]
        public int StockQuantity { get; set; }

        // --- Image Upload ---
        public string? ImagePath { get; set; }   // just filename in DB

        [NotMapped]
        [Display(Name = "Product Image")]
        public IFormFile? ImageFile { get; set; } // used for uploading, not saved in DB

        // --- SubCategory (only ID needed) ---
        [Required(ErrorMessage = "Please select a SubCategory")]
        public int SubCategoryId { get; set; }

    }
}
