using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.Products
{
    public class SubCategoryModel : PageModel
    {
        private readonly AppDBContext _context;

        public SubCategoryModel(AppDBContext context)
        {
            _context = context;
        }

        public SubCategory SubCategory { get; set; }
        public List<products_entity> Products { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            SubCategory = await _context.SubCategories
                .Include(sc => sc.Products)
                .FirstOrDefaultAsync(sc => sc.Id == id);

            if (SubCategory == null)
            {
                return NotFound();
            }

            Products = await _context.products
                .Where(p => p.SubCategoryId == id && p.StockQuantity > 0)
                .ToListAsync();

            return Page();
        }

        // Add OnPostAddToCartAsync method similar to Index page
    }
}
