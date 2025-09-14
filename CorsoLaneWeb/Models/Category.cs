using System.ComponentModel.DataAnnotations;

namespace CorsoLaneWeb.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }       // PK
        public string Name { get; set; }



        public ICollection<SubCategory> SubCategories
        {
            get; set;
            // Navigation properties

        }
    }
}