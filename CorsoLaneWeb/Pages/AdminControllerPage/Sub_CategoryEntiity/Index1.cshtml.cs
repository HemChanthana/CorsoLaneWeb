using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage.Sub_CategoryEntiity
{
    public class Index1Model(AppDBContext _db) : PageModel
    {
       

         public IList<SubCategory> SubCategories
        { get; set; }

        public async Task OnGetAsync()
        {
            // Include join table and linked categories
            SubCategories = await _db.SubCategories
            .Include(sc => sc.CategorySubCategories)
            .ThenInclude(csc => csc.Category)
            .ToListAsync();
        }

    
}
}
