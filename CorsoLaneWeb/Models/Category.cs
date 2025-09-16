using System.ComponentModel.DataAnnotations;

namespace CorsoLaneWeb.Models
{
    public class Category
    {

        [Key]
        public int Id { get; set; }     
        
        [Required]// PK
        public string Name { get; set; }

           
        public IEnumerable<SubCategory> SubCategories
        {
            get; set;
            

        }
    }
}