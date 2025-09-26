using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage.CategoryControll
{
    [Authorize(Roles = "Admin")]
    public class DeleteCategoryModel(AppDBContext db) : PageModel
    {
        [BindProperty]
        public Category Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await db.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (Category == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var category = await db.Categories.FindAsync(id);

            if (category == null)
                return NotFound();

            db.Categories.Remove(category);
            await db.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
