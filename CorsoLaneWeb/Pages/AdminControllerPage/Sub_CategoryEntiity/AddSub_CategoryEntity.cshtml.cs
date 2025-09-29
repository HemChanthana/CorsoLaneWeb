using System;
using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage.Sub_CategoryEntiity
{
    public class AddSub_CategoryEntityModel(AppDBContext db): PageModel
    {


        [BindProperty]
        public SubCategory NewSubCategory { get; set; }

        public List<SelectListItem> Categories { get; set; }

        [BindProperty]
        public int SelectedCategoryId { get; set; }

        public void OnGet()
        {
            Categories = db.Categories
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
                .ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            // Add SubCategory
            db.SubCategories.Add(NewSubCategory);
            db.SaveChanges();

            // Link SubCategory to selected Category
            if (SelectedCategoryId > 0)
            {
                var join = new CategorySubCategory
                {
                    CategoryId = SelectedCategoryId,
                    SubCategoryId = NewSubCategory.Id
                };
                // Implement a join table to links 
                db.CategorySubCategories.Add(join);
                db.SaveChanges();
            }

            return RedirectToPage("/AdminControllerPage/Sub_CategoryEntiity/Index1");
        }
    }
}
