using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CorsoLaneWeb.Pages.AdminControllerPage.CategoryControll
{
    public class AddCategoryModel(AppDBContext db) : PageModel
    {


        [BindProperty]
        public Category NewCategory { get; set; }    

        public  user userNew { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost() 
        {
            if (ModelState.IsValid)
            {
                await db.Categories.AddAsync(NewCategory);
                await db.SaveChangesAsync();

                return Page(); 

            }
            else
            {
                return Page();
            }


               
        }

    }
}
