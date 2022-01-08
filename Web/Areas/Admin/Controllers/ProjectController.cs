using Common.Extentions;
using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Infrastructure.Repositories.Application.Basic;
using MD.PersianDateTime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class ProjectController : BaseController<UserController>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly IProjectRepository _projectRepository;

        public ProjectController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IProjectRepository projectRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _projectRepository = projectRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var projectList = await _projectRepository.GetListAsync();

            var model = _mapper.Map<IEnumerable<ProjectViewModel>>(projectList);

            return PartialView("_ViewAll", model);
        }

        public async Task<IActionResult> Edit(int projectId)
        {
            var model = await _projectRepository.GetByIdAsync(projectId);

            return View(_mapper.Map<ProjectViewModel>(model));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProjectViewModel model)
        {

            if (model.Id == 0)
            {
                await _projectRepository.InsertAsync(_mapper.Map<Project>(model));
            }
            else
            {
                _mapper.Map(model, await _projectRepository.GetByIdAsync(Convert.ToInt32(model.Id)));
            }

            var res = await _projectRepository.SaveChangesAsync();

            if (res > 0)
                _notify.Success("عملیات با موفقیت انجام شد.");
            else
                _notify.Error("عملیات با خطا مواجعه شد.");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int projectId)
        {
            var model = await _projectRepository.GetByIdAsync(projectId);

            model.IsDeleted = true;

            await _projectRepository.UpdateAsync(model);

            var res = await _projectRepository.SaveChangesAsync();

            if (res > 0)
                _notify.Success("حذف پروژه با موفقیت انجام شد.");
            else
                _notify.Error("حذف پروژه انجام نشد.");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var model = await _projectRepository.GetListAsync();

            return Json(model);
        }
    }
}