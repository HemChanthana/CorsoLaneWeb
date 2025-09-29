using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage
{
    public class DashboardModel : PageModel
    {
        private readonly AppDBContext _db;
        private readonly UserManager<user> _userManager;

        public DashboardModel(AppDBContext db, UserManager<user> userManager)
        {
            _db = db;
            _userManager = userManager;
        }

        public IEnumerable<products_entity> Products { get; set; }

        public void OnGet()
        {
            Products = _db.products.ToList();
        }

        public IActionResult OnGetProductList()
        {
            var products = _db.products.ToList();
            return Partial("_ProductList", products);
        }

        public async Task<IActionResult> OnGetCustomerListAsync()
        {
            var customers = await _userManager.Users.ToListAsync();
            return Partial("_CustomerList", customers);
        }
    }
}
