using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CorsoLaneWeb.Pages.Checkout
{
    public class IndexModel : PageModel
    {
        private readonly AppDBContext _context;

        public IndexModel(AppDBContext context)
        {
            _context = context;
        }

        // Bind properties directly on the PageModel
        [BindProperty]
        [Required]
        [Display(Name = "Full Name")]
        public string CustomerName { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        [Display(Name = "Shipping Address")]
        public string ShippingAddress { get; set; } = string.Empty;

        // Cart data
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Reload cart items if validation fails
                CartItems = await GetCartItemsAsync();
                return Page();
            }

            var cartItemsForOrder = await GetCartItemsAsync();

            if (!cartItemsForOrder.Any())
            {
                ModelState.AddModelError("", "Your cart is empty");
                CartItems = cartItemsForOrder;
                return Page();
            }

            // Create order
            var order = new Order
            {
                CustomerName = CustomerName,
                CustomerEmail = CustomerEmail,
                ShippingAddress = ShippingAddress,
                TotalAmount = Total,
                OrderItems = cartItemsForOrder.Select(item => new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Product.Price
                }).ToList()
            };

            _context.Orders.Add(order);

            // Clear cart
            _context.CartItems.RemoveRange(cartItemsForOrder);

            await _context.SaveChangesAsync();

            return RedirectToPage("/OrderConfirmation", new { orderId = order.Id });
        }
    }
}
