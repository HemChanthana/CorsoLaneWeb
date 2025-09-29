using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CorsoLaneWeb.Pages.AdminControllerPage.Product_EntityControll
{
    public class DeleteProductEntityModel(AppDBContext _db) : PageModel
    {
  
        public products_entity products { get; set; }

        // Load product to confirm deletion
        public async Task<IActionResult> OnGetAsync(int id)
        {
            products = await _db.products.FindAsync(id);
            if (products == null)
            {
                return RedirectToPage("./Index"); // Redirect if product not found
            }
            return Page();
        }

        // Delete product
        public async Task<IActionResult> OnPostAsync(int id)
        {
            var product = await _db.products.FindAsync(id);
            if (product != null)
            {
                _db.products.Remove(product);
                await _db.SaveChangesAsync();
            }
            return RedirectToPage("/Index"); // Redirect to product list
        }
    }
}
