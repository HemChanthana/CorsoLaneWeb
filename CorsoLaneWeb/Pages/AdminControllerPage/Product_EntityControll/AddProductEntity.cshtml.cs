using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage.Product_EntityControll
{
    public class AddProductEntityModel : PageModel
    {
        private readonly AppDBContext _db;
        private readonly IWebHostEnvironment _environment;

        public AddProductEntityModel(AppDBContext db, IWebHostEnvironment environment)
        {
            _db = db;
            _environment = environment;
        }

        [BindProperty]
        public products_entity NewProduct { get; set; }

        // Remove this property as it's confusing the model binding
        // public int SelectedSubCategories { get; set; }

        public List<SelectListItem> SubCategoriesOption { get; set; }

        public async Task OnGet()
        {
            await GetData();
        }

        public async Task GetData()
        {
            SubCategoriesOption = await _db.CategorySubCategories
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
            // Debug: Check what values are coming through
            Console.WriteLine($"SubCategoryId from form: {NewProduct?.SubCategoryId}");

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation Error: {error.ErrorMessage}");
                }

                await GetData();
                return Page();
            }

            // Additional validation for SubCategoryId
            if (NewProduct.SubCategoryId == 0)
            {
                ModelState.AddModelError("NewProduct.SubCategoryId", "The SubCategory field is required.");
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
                NewProduct.ImagePath = null;
            }

            await _db.products.AddAsync(NewProduct);
            await _db.SaveChangesAsync();

            Console.WriteLine($"Successfully added product with SubCategoryId: {NewProduct.SubCategoryId}");

            return RedirectToPage("/AdminControllerPage/Product_EntityControll/Index");
        }
    }
}
