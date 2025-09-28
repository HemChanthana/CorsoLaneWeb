using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CorsoLaneWeb.Pages.AdminControllerPage.Sub_CategoryEntiity
{
    public class DeleteSub_CategoryModel(AppDBContext _db) : PageModel
    {



        [BindProperty]
        public SubCategory SubCategory { get; set; }

        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            SubCategory = await _db.SubCategories.FindAsync(id);
            if (SubCategory == null)
            {
                Message = "SubCategory not found.";
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var subCategory = await _db.SubCategories.FindAsync(id);
            if (subCategory != null)
            {
                _db.SubCategories.Remove(subCategory);
                await _db.SaveChangesAsync();
                return RedirectToPage("./Index1"); // Redirect to SubCategory list
            }

            Message = "SubCategory not found.";
            return Page();
        }
    }
}
