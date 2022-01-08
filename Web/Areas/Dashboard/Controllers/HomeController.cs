using Domain.Entities.Base.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Web.Abstractions;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [AllowAnonymous]
    public class HomeController : BaseController<HomeController>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (user == null)
                return LocalRedirect("~/Authentication/Login/");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Any(m => m == "User"))
            {
                return LocalRedirect("~/Dashboard/User/EditInformation");
            }

            return LocalRedirect("~/Dashboard/Managment");
        }
    }
}
