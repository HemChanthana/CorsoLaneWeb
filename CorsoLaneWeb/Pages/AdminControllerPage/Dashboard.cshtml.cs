using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CorsoLaneWeb.Pages.AdminControllerPage
{
    public class DashboardModel(AppDBContext _db) : PageModel
    {

        public IEnumerable<products_entity> Products { get; set; }
        public void OnGet()
              
        {
            Products = _db.products.ToList();
        }
        public PartialViewResult OnGetProductList()
        {
            var products = _db.products.ToList();
            return new PartialViewResult
            {
                ViewName = "_ProductList",
                ViewData = new Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<IEnumerable<products_entity>>(
                    ViewData,
                    products
                )
            };
        }



    }
}
