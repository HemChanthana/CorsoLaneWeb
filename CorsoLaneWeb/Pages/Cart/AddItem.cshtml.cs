using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.Cart
{
    // Pages/Cart/AddItem.cshtml.cs
    public class AddItemModel : PageModel
    {
        private readonly AppDBContext _context;

        public AddItemModel(AppDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AddToCartRequest Request { get; set; } = new();

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session == null)
            {
                return BadRequest("Session not available");
            }

            var product = await _context.Products.FindAsync(Request.ProductId);
            if (product == null) return NotFound();

            var sessionId = HttpContext.Session.Id;
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(item => item.SessionId == sessionId && item.ProductId == Request.ProductId);

            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductId = Request.ProductId,
                    Quantity = Request.Quantity,
                    SessionId = sessionId
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += Request.Quantity;
            }

            await _context.SaveChangesAsync();

            var cartCount = await _context.CartItems
                .Where(item => item.SessionId == sessionId)
                .SumAsync(item => item.Quantity);

            return new JsonResult(cartCount);
        }
    }

    public class AddToCartRequest
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
