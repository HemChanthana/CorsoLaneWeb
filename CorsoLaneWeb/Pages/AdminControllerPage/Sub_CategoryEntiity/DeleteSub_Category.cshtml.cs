using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CorsoLaneWeb.Pages.AdminControllerPage.Sub_CategoryEntiity
{
    public class DeleteSub_CategoryModel : PageModel
    {


        public SubCategory subCategory { get; set; }    


        public void OnGet()
        {
        }
    }
}
