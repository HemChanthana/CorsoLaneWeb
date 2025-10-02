using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CorsoLaneWeb.Pages
{


    public class IndexModel : PageModel
    {
        private readonly AppDBContext _db;

        public List<products_entity> Products { get; set; } = new();
        public Dictionary<int, string> SubCategoryMap { get; set; } = new();

        public IndexModel(AppDBContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Products = _db.products.ToList();

            SubCategoryMap = _db.SubCategories
                .ToDictionary(sc => sc.Id, sc => sc.Name);
        }
    }


}

