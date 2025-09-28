using CorsoLaneWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; // <-- important
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorsoLaneWeb.Pages.AdminControllerPage
{
    public class _CustomerListModel : PageModel
    {
        private readonly UserManager<user> _userManager;

        public _CustomerListModel(UserManager<user> userManager)
        {
            _userManager = userManager;
        }

        public List<user> Users { get; set; } = new List<user>();

        public async Task OnGetAsync()
        {
            // Fetch all users asynchronously
            Users = await _userManager.Users.ToListAsync();
        }
    }
}
