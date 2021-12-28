using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Infrastructure.Repositories.Application.Basic;
using MD.PersianDateTime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Areas.Dashboard.Models;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    [Authorize("User")]
    public class UserController : BaseController<UserController>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAdditionalUserDateRepository _additionalUserDateRepository;

        public UserController(UserManager<ApplicationUser> userManager,
            IAdditionalUserDateRepository additionalUserDateRepository)
        {
            _userManager = userManager;
            _additionalUserDateRepository = additionalUserDateRepository;
        }

        public async Task<IActionResult> EditInformation()
        {
            _notify.Information("همه فیلد ها الزامی می باشد.");

            var user = _mapper.Map<EditUserViewModel>(await _userManager.GetUserAsync(HttpContext.User));

            var pc = new PersianCalendar();
            //bug

            if (user.Birthday != null)
            {
                user.Birthday = new DateTime(pc.GetYear(user.Birthday.Value), pc.GetMonth(user.Birthday.Value), pc.GetDayOfMonth(user.Birthday.Value));
            }

            user.AdditionalUserData = _mapper.Map<List<AdditionalUserDataViewModel>>
                (_additionalUserDateRepository.Model.Where(x => x.ParentRef == user.Id))
                .Where(x => x.FamilyRole != Common.Enums.FamilyRole.Me).ToList();

            user.AdditionalUserData.ForEach(x =>
            {
                if (x.Birthday != null)                                                                                                                                                            
                    x.Birthday = new DateTime(pc.GetYear(x.Birthday.Value), pc.GetMonth(x.Birthday.Value), pc.GetDayOfMonth(x.Birthday.Value));
            });


            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            var pc = new PersianCalendar();

            var user = await _userManager.GetUserAsync(HttpContext.User);

            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            user.FatherName = model.FatherName;
            user.IdentitySerialNumber = model.IdentitySerialNumber;
            user.IdentityNumber = model.IdentityNumber;
            user.BirthPlace = model.BirthPlace;
            user.ZipCode = model.ZipCode;
            if (model.Birthday != null)
                user.Birthday = new DateTime(model.Birthday.Value.Year, model.Birthday.Value.Month, model.Birthday.Value.Day, pc);

            var res = await _userManager.UpdateAsync(user);


            if (!model.AdditionalUserData.Any(x => x.FamilyRole == Common.Enums.FamilyRole.Me))
                model.AdditionalUserData.Add(new AdditionalUserDataViewModel
                {
                    FamilyRole = Common.Enums.FamilyRole.Me,
                    ParentRef = user.Id
                });

            var additionaluserModel = _mapper.Map<List<AdditionalUserData>>(model.AdditionalUserData);
            await _additionalUserDateRepository.UpdateByUserId(additionaluserModel, user.Id);


            if (res.Succeeded)

                _notify.Success("ویرایش با موفقیت انجام شد.");
            else
                _notify.Error("ویرایش با خطا مواجعه شد .");

            return RedirectToAction("EditInformation");
        }
    }
}
