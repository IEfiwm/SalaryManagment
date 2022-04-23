using Common.Extentions;
using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Infrastructure.Base.Permission;
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
        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IProjectRepository _projectRepository;
        private readonly IBankRepository _bankRepository;
        private readonly IBank_AccountRepository _bank_AccountRepository;
        private readonly IProjectBankAccountRepository _projectBankAccountRepository;
        private readonly IPermissionCommon _permissionCommon;

        public ProjectController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IProjectRepository projectRepository,
            IBankRepository bankRepository,
            IBank_AccountRepository bank_AccountRepository,
            IProjectBankAccountRepository projectBankAccountRepository,
            IPermissionCommon permissionCommon)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _projectRepository = projectRepository;
            _bankRepository = bankRepository;
            _bank_AccountRepository = bank_AccountRepository;
            _projectBankAccountRepository = projectBankAccountRepository;
            _permissionCommon = permissionCommon;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            var banks = await _bankRepository.GetListAsync();
            ViewData["banks"] = _mapper.Map<List<BankViewModel>>(banks.Where(x => x.Active).ToList());

            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var projectList = await _permissionCommon.GetProjectsByPermission("Show", HttpContext.User);

            var model = _mapper.Map<IEnumerable<ProjectViewModel>>(projectList);

            return PartialView("_ViewAll", model);
        }

        public async Task<IActionResult> Edit(int projectId)
        {
            var banks = await _bankRepository.GetListAsync();
            ViewData["banks"] = _mapper.Map<List<BankViewModel>>(banks.Where(x => x.Active).ToList());

            var bank_Account = await _bank_AccountRepository.GetListAsync();
            ViewData["bankAccounts"] = _mapper.Map<List<Bank_AccountViewModel>>(bank_Account.Where(x => x.Active).ToList());

            var model = await _projectRepository.GetWithBankAccountsById(projectId);
            var viewModel = _mapper.Map<ProjectViewModel>(model);

            viewModel.projectBanks = new List<ProjectBankAccountViewModel>();

            if (model.ProjectBankAccounts.Any())
            {
                foreach (var projectBank in model.ProjectBankAccounts)
                {
                    viewModel.projectBanks.Add(new ProjectBankAccountViewModel
                    {
                        ProjectId = model.Id,
                        Bank_AccountId = projectBank.Bank_AccountId,
                        BankId = projectBank.Bank_Account.BankId
                    });
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProjectViewModel model)
        {
            try
            {

                bool isUpdate = false;
                Project projectModel = _mapper.Map<Project>(model);

                if (model.Id == 0)
                {
                    await _projectRepository.InsertAsync(projectModel);
                }
                else
                {
                    isUpdate = true;
                    projectModel = await _projectRepository.GetByIdAsync(Convert.ToInt32(model.Id));
                    _mapper.Map(model, projectModel);
                    projectModel.BranchName = model.BranchName;
                }


                if (model.BankAccounts != null && model.BankAccounts.Count > 0)
                {
                    var bankIdList = new List<long>();
                    foreach (var acc in model.BankAccounts)
                    {
                        var bankAcc = await _bank_AccountRepository.GetByAccountId(acc);
                        if (bankAcc is not null)
                        {
                            if (!bankIdList.Any(x => x == bankAcc.Id))
                                bankIdList.Add(bankAcc.Id);
                            else
                            {
                                _notify.Error("عملیات با خطا مواجعه شد، امکان انتخاب دو بانک مشابه برای یک پروژه وجود ندارد.");
                                return RedirectToAction("Index");
                            }
                        }
                    }

                }


                var res = await _projectRepository.SaveChangesAsync();

                if (isUpdate)
                {
                    var resRemove = await _projectBankAccountRepository.RemoveAccountsFromProject(projectModel.Id);

                }

                if (model.BankAccounts != null && model.BankAccounts.Count > 0)
                {

                    var resAdd = await _projectBankAccountRepository.SetAccountsToProject(model.BankAccounts, projectModel.Id);

                    if (resAdd > 0)
                        _notify.Success("عملیات با موفقیت انجام شد.");
                    else
                        _notify.Error("عملیات با خطا مواجعه شد.");
                }
                else
                {
                    _notify.Success("عملیات با موفقیت انجام شد.");
                }


            }
            catch (Exception x)
            {

                _notify.Error("عملیات با خطا مواجعه شد.");
            }

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
            var model = await _permissionCommon.GetProjectsByPermission("Show", HttpContext.User);
            return Json(model);
        }

    }
}