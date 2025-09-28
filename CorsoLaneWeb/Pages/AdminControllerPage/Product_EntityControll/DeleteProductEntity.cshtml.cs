using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CorsoLaneWeb.Pages.AdminControllerPage.Product_EntityControll
{
    public class DeleteProductEntityModel(AppDBContext db) : PageModel
    {
        public products_entity Product_Entity { get; set; }
        public async Task OnGetAsnyc(int id)
        {
            
        }
    }
}
