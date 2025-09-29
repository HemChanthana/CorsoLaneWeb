using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly AppDBContext _context;
        private readonly UserManager<user> _userManager;

        public IndexModel(AppDBContext context, UserManager<user> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<products_entity> Products { get; set; } = new();
        public string SearchTerm { get; set; }
        public string Filter { get; set; } = "all";

        public async Task OnGetAsync(string search, string filter)
        {
            SearchTerm = search;
            Filter = filter ?? "all";

            var query = _context.products
                .Include(p => p.SubCategory)
                .Where(p => p.StockQuantity > 0)
                .AsQueryable();

            // Apply search filter
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p =>
                    p.Name.Contains(search) ||
                    p.Description.Contains(search) ||
                    p.Color.Contains(search));
            }

            // Apply other filters
            switch (filter)
            {
                case "new":
                    // Assuming you have a CreatedDate property
                    query = query.OrderByDescending(p => p.Id).Take(10);
                    break;
                case "popular":
                    // You might want to add an OrderCount property later
                    query = query.OrderByDescending(p => p.Id).Take(10);
                    break;
                case "sale":
                    // You might want to add a IsOnSale property later
                    query = query.Where(p => p.Price < 50); // Example sale filter
                    break;
            }

            Products = await query.ToListAsync();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int productId, int quantity)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity", returnUrl = "/Products" });
            }

            var user = await _userManager.GetUserAsync(User);
            var product = await _context.products.FindAsync(productId);

            if (product == null || product.StockQuantity < quantity)
            {
                TempData["Error"] = "Product not available or insufficient stock.";
                return RedirectToPage();
            }

            var existingCartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.UserId == user.Id && ci.ProductId == productId);

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += quantity;
            }
            else
            {
                var cartItem = new CartItem
                {
                    UserId = user.Id,
                    ProductId = productId,
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }

            await _context.SaveChangesAsync();
            TempData["Success"] = $"{product.Name} added to cart!";
            return RedirectToPage();
        }
    }
}
