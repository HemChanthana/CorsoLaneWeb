using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorsoLaneWeb.Models
{
    public class SubCategory


    {
        [Key]
        public int Id { get; set; } // Primary Key
        public string Name { get; set; }
        public string Description { get; set; }

        // --- Relationship Properties ---

        // 1. Foreign Key to the Category class
        public int CategoryId { get; set; }

        // 2. Navigation property to the parent Category
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
