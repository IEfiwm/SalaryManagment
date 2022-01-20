using Application.Enums;
using Common.Helpers;
using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Infrastructure.Repositories.Application.Basic;
using Infrastructure.Repositories.Application.Idenitity;
using MD.PersianDateTime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class UserController : BaseController<UserController>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IAdditionalUserDateRepository _additionalUserDateRepository;


        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUserRepository userRepository,
            IBankAccountRepository bankAccountRepository,
            IAdditionalUserDateRepository additionalUserDateRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _bankAccountRepository = bankAccountRepository;
            _additionalUserDateRepository = additionalUserDateRepository;
        }

        //[Authorize(Policy = Permissions.Users.View)]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var currentUser = await _userRepository.GetUserAsync(HttpContext.User);

            var role = await _roleManager.FindByNameAsync(Roles.User.ToString());

            var allUsersExceptCurrentUser = await _userRepository.GetUserListAsync();

            var model = _mapper.Map<IEnumerable<UserViewModel>>(allUsersExceptCurrentUser.Where(m => m.Email is null && !m.IsDeleted));

            foreach (var user in model)
            {
                try
                {

                    user.HasAdditionalUser = _additionalUserDateRepository.HasAdditionalUsers(user.Id);
                    user.HasAdditionalUserDocument = _additionalUserDateRepository.HasAdditionalUserDocument(user.Id);
                    user.HasDocument = _additionalUserDateRepository.HasDocuments(user.Id);

                }
                catch (Exception x)
                {

                    throw;
                }
            }


            return PartialView("_ViewAll", model.ToList());
        }

        public async Task<IActionResult> OnGetCreate()
        {
            return new JsonResult(new { isValid = true, html = await _viewRenderer.RenderViewToStringAsync("_Create", new UserViewModel()) });
        }

        [HttpPost]
        public async Task<IActionResult> OnPostCreate(UserViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                MailAddress address = new MailAddress(userModel.Email);
                string userName = address.User;
                var user = new ApplicationUser
                {
                    UserName = userName,
                    Email = userModel.Email,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    EmailConfirmed = true,
                };
                var result = await _userManager.CreateAsync(user, userModel.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.User.ToString());
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var currentUser = await _userManager.GetUserAsync(HttpContext.User);
                    var allUsersExceptCurrentUser = await _userManager.Users.Where(a => a.Id != currentUser.Id).ToListAsync();
                    var users = _mapper.Map<IEnumerable<UserViewModel>>(allUsersExceptCurrentUser);
                    var htmlData = await _viewRenderer.RenderViewToStringAsync("_ViewAll", users);
                    _notify.Success($"Account for {user.Email} created.");
                    return new JsonResult(new { isValid = true, html = htmlData });
                }
                foreach (var error in result.Errors)
                {
                    _notify.Error(error.Description);
                }
                var html = await _viewRenderer.RenderViewToStringAsync("_Create", userModel);
                return new JsonResult(new { isValid = false, html = html });
            }
            return default;
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var model = _mapper.Map<UserViewModel>(user);
            if (user.BankAccountRef != null)
            {
                var acc = await _bankAccountRepository.GetByIdAsync(user.BankAccountRef.Value);
                model.BankAccNumber = acc.AccountNumber;
                model.BankName = acc.Title;
            }
            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel user)
        {
            var bankRef = await _bankAccountRepository.InsertAndSaveAsync(new BankAccount
            {
                AccountNumber = user.BankAccNumber,
                Title = user.BankName,
                CreatedByRef = (await _userManager.GetUserAsync(HttpContext.User)).Id,
                CreatedDate = System.DateTime.Now
            });

            user.UserName = user.PhoneNumber;

            var ouser = await _userRepository.GetUserByIdAsync(user.Id);

            ouser.BankAccountRef = bankRef;

            ouser = _mapper.Map<UserViewModel, ApplicationUser>(user, ouser);

            var res = await _userRepository.SaveChangesAsync();

            //var res = await _userManager.UpdateAsync(ouser);

            if (res > 0)
                _notify.Success("ویرایش با موفقیت انجام شد.");
            else
            {
                _notify.Error("ویرایش انجام نشد.");

                return Redirect("/admin/user/edit?userId=" + user.Id);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel user)
        {
            var bankRef = await _bankAccountRepository.InsertAndSaveAsync(new BankAccount
            {
                AccountNumber = user.BankAccNumber,
                Title = user.BankName,
                CreatedByRef = (await _userManager.GetUserAsync(HttpContext.User)).Id,
                CreatedDate = System.DateTime.Now
            });

            user.Id = Guid.NewGuid().ToString();

            user.UserName = user.PersonnelCode ?? user.NationalCode;

            if (await _userRepository.Model.Where(m => m.UserName == user.UserName || m.PersonnelCode == user.PersonnelCode || m.NationalCode == user.NationalCode || m.PhoneNumber == user.PhoneNumber).FirstOrDefaultAsync() != null)
            {
                _notify.Error("افزودن کاربر انجام نشد.");

                _notify.Error("کاربر با این مشخصات در سیستم موجود می باشد.");

                return RedirectToAction("Index");
            }

            ApplicationUser model = _mapper.Map<ApplicationUser>(user);

            model.BankAccountRef = bankRef;

            model.CreateDate = System.DateTime.Now;

            model.IsDeleted = false;

            model.IsProfileCompleted = true;

            model.IsInsurance = true;

            var res = await _userManager.CreateAsync(model, model.PersonnelCode);

            if (res.Succeeded)
            {
                await _userManager.AddToRoleAsync(model, Roles.User.ToString());

                _notify.Success("افزودن کاربر با موفقیت انجام شد.");
            }
            else
            {
                _notify.Error("افزودن کاربر انجام نشد.");

                if (user.Id is null)
                {
                    RedirectToAction("Index");
                }

                return Redirect("/admin/user/edit?userId=" + user.Id);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.IsDeleted = true;

            var res = await _userManager.UpdateAsync(user);

            if (res.Succeeded)
            {
                await _bankAccountRepository.SoftDeleteAsync(user.BankAccountRef.Value);

                await _bankAccountRepository.SaveChangesAsync();
            }
            else
                _notify.Error("حذف کاربر انجام نشد.");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ContractList(string startDate, string endDate, long projectId = 0)
        {
            ViewData["projectId"] = projectId;
            var model = new ContractListViewModel();

            if (projectId != 0)
            {
                var usersByprojectId = await _userRepository.GetUserListByProjectIdAsync(projectId);

                model.Users = _mapper.Map<IEnumerable<UserViewModel>>(usersByprojectId).ToList();
            }

            model.StartDate = startDate;

            model.EndDate = endDate;

            model.ProjectId = projectId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllContractsFile(ContractListParameters model)
        {
            var usersByprojectId = await _userRepository.GetUserListByProjectIdAsync(model.projectId);

            if (usersByprojectId.Count == 0)
            {
                _notify.Error("کاربری یافت نشد.");
                return RedirectToAction("contractlist");
            }

            model.usernameList = usersByprojectId.Select(X => X.NationalCode).ToList();

            var path = Path.Combine(
    Directory.GetCurrentDirectory(),
    "wwwroot", "AllContracts_" + DateTime.Now.Year + CommonHelper.GetTowDigits(DateTime.Now.Month) + ".zip");

            var memory = new MemoryStream();

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromHours(6);

                var json = JsonConvert.SerializeObject(model);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($@"{ _configuration["Base:KoshaCore:APIAddress"].ToString()}/Contract/GetAll", data))
                {
                    var test = await response.Content.ReadAsByteArrayAsync();

                    await System.IO.File.WriteAllBytesAsync(path, test);

                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }

                    memory.Position = 0;

                    System.IO.File.Delete(path);
                }
            }
            return File(memory, "application/zip", Path.GetFileName(path));
        }
    }
}