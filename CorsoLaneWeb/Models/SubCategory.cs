using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoLaneWeb.Models
{
    public class SubCategory


    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CategorySubCategory> CategorySubCategories { get; set; } = new List<CategorySubCategory>();
        public ICollection<products_entity> Products { get; set; } = new List<products_entity>();
    }
}
