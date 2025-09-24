using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage.Product_EntityControll
{


  
    public class AddProductEntityModel(AppDBContext db) : PageModel
    {

        [BindProperty]
        public products_entity NewProduct { get; set; }

        public List<SelectListItem> SubCategories { get; set; }

        public void OnGet()
        {
            SubCategories = db.SubCategories
                .Select(sc => new SelectListItem { Value = sc.Id.ToString(), Text = sc.Name })
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            db.products.Add(NewProduct);
            db.SaveChanges();

         
            return RedirectToPage("/AdminControllerPage/Product_EntityControll/ProductEntityList");
        }   
    }
}
