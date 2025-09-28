using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage.Product_EntityControll
{
    public class EditProductEntityModel(AppDBContext db) : PageModel
    {
        [BindProperty]
        public Product EditProduct { get; set; }

        public SelectList SubCategoriesOption { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            EditProduct = await db.Products.FindAsync(id);

            if (EditProduct == null)
            {
                return NotFound();
            }

            SubCategoriesOption = new SelectList(db.SubCategories, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                SubCategoriesOption = new SelectList(db.SubCategories, "Id", "Name");
                return Page();
            }

            var productInDb = await db.Products.FindAsync(EditProduct.Id);
            if (productInDb == null)
            {
                return NotFound();
            }

            // Update fields
            productInDb.Name = EditProduct.Name;
            productInDb.Description = EditProduct.Description;
            productInDb.Size = EditProduct.Size;
            productInDb.Color = EditProduct.Color;
            productInDb.Price = EditProduct.Price;
            productInDb.StockQuantity = EditProduct.StockQuantity;
            productInDb.SubCategoryId = EditProduct.SubCategoryId;

            // Handle image upload if new file uploaded
            if (EditProduct.ImageFile != null)
            {
                var fileName = Path.GetFileName(EditProduct.ImageFile.FileName);
                var filePath = Path.Combine("wwwroot/uploads", fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await EditProduct.ImageFile.CopyToAsync(stream);
                }

                productInDb.ImagePath = "wwwroot/uploads" + fileName;
            }

            await db.SaveChangesAsync();

            return RedirectToPage("Index"); // back to product list
        }
    }
}
