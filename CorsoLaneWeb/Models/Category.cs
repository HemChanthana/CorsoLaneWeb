using System.ComponentModel.DataAnnotations;

namespace CorsoLaneWeb.Models
{
    public class Category
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // "Men" or "Women"
        public ICollection<SubCategory>? SubCategories { get; set; }



        // Many-to-many with SubCategory via join table

        public ICollection<CategorySubCategory> CategorySubCategories { get; set; } = new List<CategorySubCategory>(); 
    }
}