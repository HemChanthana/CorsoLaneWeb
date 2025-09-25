using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CorsoLaneWeb.Pages.AdminControllerPage.Product_EntityControll
{
    public class IndexModel(AppDBContext db): PageModel
    {

       public IEnumerable<products_entity>  products { get; set; }
        public async Task OnGet()
        {
            products = await db.products.ToListAsync();
        }
    }
}
