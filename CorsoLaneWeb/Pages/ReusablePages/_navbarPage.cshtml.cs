using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.ReusablePages
{
    public class navbarPageModel : PageModel
    {
        private readonly AppDBContext _db;

        public navbarPageModel(AppDBContext db)
        {
            _db = db;
        }

        public List<Category> Categories { get; set; }
        public Dictionary<int, List<SubCategory>> CategorySubCategories { get; set; } = new();
        public int CartItemCount { get; set; }

        public async Task OnGetAsync()
        {
            // Load categories with their subcategories
            Categories = await _db.Categories
                .Include(c => c.CategorySubCategories)
                .ThenInclude(cs => cs.SubCategory)
                .ToListAsync();

            // Organize subcategories by category
            foreach (var category in Categories)
            {
                var subCategories = category.CategorySubCategories
                    .Select(cs => cs.SubCategory)
                    .ToList();
                CategorySubCategories[category.Id] = subCategories;
            }

            // Get cart count for authenticated users
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userId))
                {
                    CartItemCount = await _db.CartItems
                        .Where(ci => ci.UserId == userId)
                        .SumAsync(ci => ci.Quantity);
                }
            }
        }
    }
}
