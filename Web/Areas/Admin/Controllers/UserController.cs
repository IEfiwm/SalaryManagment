﻿using Application.Enums;
using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Infrastructure.Repositories.Application.Basic;
using Infrastructure.Repositories.Application.Idenitity;
using MD.PersianDateTime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
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
        private readonly IProjectRepository _projectRepository;

        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IUserRepository userRepository,
            IBankAccountRepository bankAccountRepository,
            IProjectRepository projectRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _projectRepository = projectRepository;
            _bankAccountRepository = bankAccountRepository;
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

            var model = _mapper.Map<IEnumerable<UserViewModel>>(allUsersExceptCurrentUser);

            return PartialView("_ViewAll", model.Where(m => m.Email is null).ToList());
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

            return View("Edit", _mapper.Map<UserViewModel>(user));
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(UserViewModel user)
        {
            user.UserName = user.PhoneNumber;

            var ouser = await _userRepository.GetUserByIdAsync(user.Id);

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

            user.UserName = user.PhoneNumber;

            ApplicationUser model = _mapper.Map<ApplicationUser>(user);

            model.BankAccountRef = bankRef;

            model.CreateDate = System.DateTime.Now;

            model.IsDeleted = false;

            model.IsProfileCompleted = true;

            model.IsInsurance = true;

            var res = await _userManager.CreateAsync(model, model.PhoneNumber);

            if (res.Succeeded)
            {
                await _userManager.AddToRoleAsync(model, Roles.User.ToString());

                _notify.Success("افزودن کاربر با موفقیت انجام شد.");
            }
            else
            {
                _notify.Error("افزودن کاربر انجام نشد.");

                return Redirect("/admin/user/edit?userId=" + user.Id);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var res = await _userManager.DeleteAsync(user);

            if (res.Succeeded)
                _notify.Success("حذف کاربر با موفقیت انجام شد.");
            else
                _notify.Error("حذف کاربر انجام نشد.");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ContractList(long projectId = 0)
        {
            ViewData["projectId"] = projectId;
            var model = new List<UserViewModel>();

            if (projectId != 0)
            {
                var usersByprojectId = await _userRepository.GetUserListByProjectIdAsync(projectId);
                model = _mapper.Map<IEnumerable<UserViewModel>>(usersByprojectId).ToList();
            }

            return View(model);

        }
    }
}