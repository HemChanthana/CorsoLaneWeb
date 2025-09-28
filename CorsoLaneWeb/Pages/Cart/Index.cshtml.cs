using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.Cart
{
    public class IndexModel : PageModel
    {
        private readonly AppDBContext _context;

        public IndexModel(AppDBContext context)
        {
            _context = context;
        }

        // These properties replace the ViewModel
        public List<CartItem> CartItems { get; set; } = new();
        public decimal Total => CartItems.Sum(item => item.Product?.Price * item.Quantity ?? 0);

        public async Task OnGetAsync()
        {
            CartItems = await GetCartItemsAsync();
        }

        private async Task<List<CartItem>> GetCartItemsAsync()
        {
            if (HttpContext.Session == null)
            {
                return new List<CartItem>();
            }

            var sessionId = HttpContext.Session.Id;
            return await _context.CartItems
                .Include(item => item.Product)
                .Where(item => item.SessionId == sessionId)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostUpdateQuantityAsync(int itemId, int quantity)
        {
            var cartItem = await _context.CartItems
                .Include(ci => ci.Product)
                .FirstOrDefaultAsync(ci => ci.Id == itemId && ci.SessionId == HttpContext.Session.Id);

            if (cartItem != null)
            {
                if (quantity <= 0)
                {
                    _context.CartItems.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = quantity;
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveItemAsync(int itemId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.Id == itemId && ci.SessionId == HttpContext.Session.Id);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
