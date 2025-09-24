using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage.CategoryControll
{
    public class IndexModel(AppDBContext db) : PageModel



    {
        public IEnumerable<Category> Categories { get; set; }


        public async Task OnGet()
        {
            Categories = await db.Set<Category>().ToListAsync();
        }
    }
}
