using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage
{
    public class AddLinkingCategoryPageModel(AppDBContext db) : PageModel
    {
        [BindProperty]
        public CategorySubCategory JoinEntity { get; set; } = new CategorySubCategory();

        public SelectList Categories { get; set; }
        public SelectList SubCategories { get; set; }

        public async Task OnGetAsync()
        {
            Categories = new SelectList(await db.Categories.ToListAsync(), "Id", "Name");
            SubCategories = new SelectList(await db.SubCategories.ToListAsync(), "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // reload dropdowns
                return Page();
            }

            db.Add(JoinEntity);
            await db.SaveChangesAsync();

            return RedirectToPage("/Admin/Index"); // go back to dashboard
        }
    }
}

