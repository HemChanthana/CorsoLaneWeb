using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage.Product_EntityControll
{
    public class IndexModel(AppDBContext _db): PageModel
    {
        // List of products to display

        public List<products_entity> Products { get; set; } = new List<products_entity>();

        public Dictionary<int, string> SubCategoryMap { get; set; } = new();

        public async Task OnGetAsync()
        {
            // Load products
            Products = await _db.products.ToListAsync();

            // Load subcategories
            var subCategories = await _db.SubCategories.ToListAsync();
            SubCategoryMap = subCategories.ToDictionary(s => s.Id, s => s.Name); 
        }
    }
}
