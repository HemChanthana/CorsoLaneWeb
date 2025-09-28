using System.ComponentModel.DataAnnotations;

namespace CorsoLaneWeb.Models
{
    public class Category
    {

        public int Id { get; set; }
        public string Name { get; set; } // "Men" or "Women"
        public ICollection<SubCategory>? SubCategories { get; set; }




        public ICollection<CategorySubCategory> CategorySubCategories { get; set; } = new List<CategorySubCategory>(); 
    }
}