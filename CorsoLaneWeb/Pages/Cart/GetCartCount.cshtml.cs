using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.Cart
{
    public class GetCartCountModel : PageModel
    {
        private readonly AppDBContext _context;
        private readonly UserManager<user> _userManager;

        public GetCartCountModel(AppDBContext context, UserManager<user> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<JsonResult> OnGetAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                var count = await _context.CartItems
                    .Where(ci => ci.UserId == user.Id)
                    .SumAsync(ci => ci.Quantity);
                return new JsonResult(new { count });
            }
            return new JsonResult(new { count = 0 });
        }
    }
}
