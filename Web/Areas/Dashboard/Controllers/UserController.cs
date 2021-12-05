using Domain.Entities.Base.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Models;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize("User")]
    public class UserController : BaseController<UserController>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> EditInformation()
        {
            _notify.Information("همه فیلد ها الزامی می باشد.");

            var user = await _userManager.GetUserAsync(HttpContext.User);

            return View(user);
        }

        [HttpGet]
        public IActionResult Edit(EditUserViewModel model)
        {
            return View();
        }
    }
}
