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
           
        }
    }
}
