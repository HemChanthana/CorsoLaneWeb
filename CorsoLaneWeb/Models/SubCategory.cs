using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoLaneWeb.Models
{
    public class SubCategory


    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // "Shirts", "Pants", etc.
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public ICollection<Product>? Products { get; set; }

        public ICollection<CategorySubCategory> CategorySubCategories { get; set; } = new List<CategorySubCategory>();
        // public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
