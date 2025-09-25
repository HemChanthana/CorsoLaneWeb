using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CorsoLaneWeb.Pages.AdminControllerPage.CategoryControll
{

    [Authorize(Roles = "Admin")]

    public class EditCategoryModel(AppDBContext db) : PageModel
    {
        


        public Category category { get; set;  }

        public async Task OnGet(int id)
        {

            category = await db.Categories.FindAsync(id); 
            
        }

        public async Task<IActionResult> onPost() 
        {

            if (ModelState.IsValid)
            {
                var existState = await db.Categories.FindAsync(category.Id);
                existState.Name = category.Name;
                await db.SaveChangesAsync();
                return RedirectToPage("/categoryControll/Index");

            }
            return Page();


        }
    }
}
