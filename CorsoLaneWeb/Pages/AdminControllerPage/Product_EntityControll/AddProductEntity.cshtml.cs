using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage.Product_EntityControll
{
    public class AddProductEntityModel(AppDBContext db, IWebHostEnvironment _environment) : PageModel
    {
        [BindProperty]
        public Product NewProduct { get; set; }

        public List<SelectListItem> SubCategoriesOption { get; set; }

        public Category Category { get; set; }

        public int SelectedSubCategories { get; set; }

        public async Task OnGet()
        {
            await GetData();
        }

        public async Task GetData()
        {
            SubCategoriesOption = await db.CategorySubCategories
                .Include(csc => csc.Category)
                .Include(csc => csc.SubCategory)
                .Select(csc => new SelectListItem
                {
                    Value = csc.SubCategory.Id.ToString(),
                    Text = csc.SubCategory.Name + " (" + csc.Category.Name + ")",
                })
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }

                await GetData();
                return Page();
            }

            Console.WriteLine($"--- The selected SubCategory ID is: {NewProduct.SubCategoryId} ---");

            if (NewProduct.ImageFile != null && NewProduct.ImageFile.Length > 0)
            {
                var uploadFolder = Path.Combine(_environment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                var fileName = Path.GetFileNameWithoutExtension(NewProduct.ImageFile.FileName);
                var extension = Path.GetExtension(NewProduct.ImageFile.FileName);
                var uploadFileName = $"{fileName}_{Guid.NewGuid()}{extension}";
                var filePath = Path.Combine(uploadFolder, uploadFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await NewProduct.ImageFile.CopyToAsync(fileStream);
                }

                NewProduct.ImagePath = uploadFileName;
            }
            else
            {
                // optional: use placeholder
                NewProduct.ImagePath = null;
            }

            await db.Products.AddAsync(NewProduct);
            await db.SaveChangesAsync();

            Console.WriteLine($"Successfully added product with SubCategoryId: {NewProduct.SubCategoryId}");

            return RedirectToPage("/AdminControllerPage/Product_EntityControll/Index");
        }
    }
}
