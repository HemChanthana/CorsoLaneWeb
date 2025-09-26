using System.Linq;
using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage.Sub_CategoryEntiity
{
    public class EditSubCategoryModel(AppDBContext _db) : PageModel
    {
        

        [BindProperty]
        public SubCategory SubCategory { get; set; }

        [BindProperty]
        public int SelectedCategoryId { get; set; }

        public List<SelectListItem> Categories { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            SubCategory = _db.SubCategories
                .FirstOrDefault(sc => sc.Id == id);

            if (SubCategory == null)
            {
                return NotFound();
            }

            // Load categories
            LoadCategories();

            // Get current linked category
            var currentLink = _db.CategorySubCategories
                .FirstOrDefault(cs => cs.SubCategoryId == SubCategory.Id);

            if (currentLink != null)
            {
                SelectedCategoryId = currentLink.CategoryId;
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return Page();
            }

            // Update SubCategory name
            var existingSub = _db.SubCategories
                .FirstOrDefault(sc => sc.Id == SubCategory.Id);

            if (existingSub == null)
                return NotFound();

            existingSub.Name = SubCategory.Name;
            _db.SaveChanges();

            // Update Category link
            var existingLink = _db.CategorySubCategories
                .FirstOrDefault(cs => cs.SubCategoryId == existingSub.Id);

            if (existingLink != null)
            {
                existingLink.CategoryId = SelectedCategoryId;
            }
            else
            {
                _db.CategorySubCategories.Add(new CategorySubCategory
                {
                    CategoryId = SelectedCategoryId,
                    SubCategoryId = existingSub.Id
                });
            }

            _db.SaveChanges();

            return RedirectToPage("/Admin/ListSubCategories");
        }

        private void LoadCategories()
        {
            Categories = _db.Categories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToList();
        }
    }
}
