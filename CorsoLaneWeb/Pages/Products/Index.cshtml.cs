using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly AppDBContext _context;

        public IndexModel(AppDBContext context)
        {
            _context = context;
        }

        public string? Category { get; set; }
        public string? SubCategory { get; set; }
        public List<Product> Products { get; set; } = new();
        public List<Category> Categories { get; set; } = new();

        public async Task OnGetAsync(string? category, string? subcategory)
        {
            Category = category;
            SubCategory = subcategory;

            var query = _context.Products
                .Include(p => p.SubCategory)
                .ThenInclude(sc => sc.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.SubCategory.Category.Name == category);
            }

            if (!string.IsNullOrEmpty(subcategory))
            {
                query = query.Where(p => p.SubCategory.Name == subcategory);
            }

            Products = await query.ToListAsync();
            Categories = await _context.Categories.Include(c => c.SubCategories).ToListAsync();
        }
    }
}
