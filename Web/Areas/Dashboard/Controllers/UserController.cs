using Domain.Entities.Base.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Globalization;
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

            var pc = new PersianCalendar();
            //bug

            if (user.Birthday != null)
            {
                var birth = user.Birthday.Value.Date.ToString("yyyy/MM/dd").Split("/");

                user.Birthday = new DateTime(Convert.ToInt32(birth[0]), Convert.ToInt32(birth[1]), Convert.ToInt32(birth[2]), new PersianCalendar());
            }


            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var pc = new GregorianCalendar();

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            user.NumberOfChildren = model.NumberOfChildren;
            user.FatherName = model.FatherName;
            user.NationalCode = model.NationalCode;
            user.Birthday = DateTime.Parse(string.Format("{0}/{1}/{2}", pc.GetYear(model.Birthday.Value), pc.GetMonth(model.Birthday.Value), pc.GetDayOfMonth(model.Birthday.Value)));
            user.IdentitySerialNumber = model.IdentitySerialNumber;
            user.IdentityNumber = model.IdentityNumber;
            user.BirthPlace = model.BirthPlace;
            user.ZipCode = model.ZipCode;
            user.InsuranceCode = model.InsuranceCode;

            var res = await _userManager.UpdateAsync(user);

            if (res.Succeeded)

                _notify.Success("ویرایش با موفقیت انجام شد.");
            else
                _notify.Error("ویرایش با خطا مواجعه شد .");

            return RedirectToAction("EditInformation");
        }
    }
}
