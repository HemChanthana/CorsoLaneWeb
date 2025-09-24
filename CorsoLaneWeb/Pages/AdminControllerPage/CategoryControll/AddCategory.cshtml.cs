using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage.CategoryControll
{


    [Authorize(Roles = "Admin")]
    public class AddCategoryModel(AppDBContext db) : PageModel
    {

        [BindProperty]
        public Category NewCategory { get; set; }

        // Optional: select SubCategories
        [BindProperty]
        public List<int> SelectedSubCategoryIds { get; set; } = new List<int>();

        public List<SelectListItem> SubCategoriesList { get; set; }
        public void OnGet()
        {

        }
            
       
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            // Add category asynchronously
            await db.Categories.AddAsync(NewCategory);
            await db.SaveChangesAsync();

            return RedirectToPage("/Index");
        }

    }
}
