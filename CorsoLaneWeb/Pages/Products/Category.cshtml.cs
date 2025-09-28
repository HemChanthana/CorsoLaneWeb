using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.Products
{
    public class CategoryModel : PageModel
    {
        private readonly AppDBContext _context;

        public CategoryModel(AppDBContext context)
        {
            _context = context;
        }

        public Category Category { get; set; }
        public List<SubCategory> SubCategories { get; set; }
        public List<products_entity> Products { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await _context.Categories
                .Include(c => c.CategorySubCategories)
                .ThenInclude(cs => cs.SubCategory)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Category == null)
            {
                return NotFound();
            }

            // Get subcategories for this category
            SubCategories = await _context.CategorySubCategories
                .Where(cs => cs.CategoryId == id)
                .Select(cs => cs.SubCategory)
                .ToListAsync();

            // Get products in this category (through subcategories)
            var subCategoryIds = SubCategories.Select(sc => sc.Id).ToList();
            Products = await _context.products
                .Include(p => p.SubCategory)
                .Where(p => subCategoryIds.Contains(p.SubCategoryId) && p.StockQuantity > 0)
                .ToListAsync();

            return Page();
        }

        // Add OnPostAddToCartAsync method similar to Index page
    }
}
