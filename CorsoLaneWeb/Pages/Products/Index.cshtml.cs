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

        public List<Category> Categories { get; set; }
        public List<products_entity> Products { get; set; }

        public async Task OnGetAsync()
        {
            Categories = await _context.Categories
                .Include(c => c.CategorySubCategories)
                .ThenInclude(cs => cs.SubCategory)
                .ToListAsync();

            Products = await _context.products
                .Include(p => p.SubCategory)
                .Where(p => p.StockQuantity > 0)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int productId, int quantity)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
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
            TempData["Success"] = "Product added to cart!";
            return RedirectToPage();
        }
    }
}
