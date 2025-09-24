using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoLaneWeb.Models
{
    public class products_entity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public string Size { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string ImagePath { get; set; }

        [NotMapped]
        [Display(Name = "Product Image")]
        public IFormFile ImageUrl { get; set; }

        public int StockQuantity { get; set; }



        // Each Product belongs to ONE SubCategory
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
