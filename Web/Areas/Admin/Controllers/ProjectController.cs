using Domain.Entities.Basic;
using Infrastructure.Base.Permission;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Application;
using Infrastructure.Repositories.Application.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProjectController : BaseController<UserController>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IBankRepository _bankRepository;
        private readonly IBank_AccountRepository _bank_AccountRepository;
        private readonly IProjectBankAccountRepository _projectBankAccountRepository;
        private readonly IPermissionCommon _permissionCommon;
        private readonly IFileRepository _fileRepository;
        private readonly IimportedRepository _iimportedRepository;

        public ProjectController(
            IProjectRepository projectRepository,
            IBankRepository bankRepository,
            IBank_AccountRepository bank_AccountRepository,
            IProjectBankAccountRepository projectBankAccountRepository,
            IPermissionCommon permissionCommon,
            IFileRepository fileRepository,
            IimportedRepository iimportedRepository)
        {
            _projectRepository = projectRepository;
            _bankRepository = bankRepository;
            _bank_AccountRepository = bank_AccountRepository;
            _projectBankAccountRepository = projectBankAccountRepository;
            _permissionCommon = permissionCommon;
            _fileRepository = fileRepository;
            _iimportedRepository = iimportedRepository;
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

            var permission = await _permissionCommon.CheckProjectPermissionByProjectId("EditProject", User, projectId);
            if (!permission)
            {
                _notify.Error(_localizer["AccessDeniedProject"].Value);
                return RedirectToAction("Index");
            }

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

                //save logo
                if (model.Logo != null)
                {
                    model.LogoPath = await _fileRepository.SaveImageAsync(model.Logo, _configuration["Base:KoshaCore:FilePath"].ToString());
                }

                if (model.Id == 0)
                {
                    projectModel.Id = await _projectRepository.InsertAndSaveAsync(projectModel);

                    await _permissionCommon.SetFullPermissionsProjectsToUser(projectModel, User);
                }
                else
                {
                    var permission = await _permissionCommon.CheckProjectPermissionByProjectId("EditProject", User, model.Id);
                    if (!permission)
                    {
                        _notify.Error(_localizer["AccessDeniedProject"].Value);
                        return RedirectToAction("Index");
                    }

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
            var permission = await _permissionCommon.CheckProjectPermissionByProjectId("DeleteProject", User, projectId);
            if (!permission)
            {
                _notify.Error(_localizer["AccessDeniedProject"].Value);
                return RedirectToAction("Index");
            }

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
        public async Task<IActionResult> GetList(string permissionName = "Show")
        {
            var projectList = await _permissionCommon.GetProjectsByPermission(permissionName, HttpContext.User);

            var model = _mapper.Map<IEnumerable<ProjectViewModel>>(projectList);

            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetThumbImage(string path)
        {
            var model = _fileRepository.GetFileFullPath(path, _configuration["Base:KoshaCore:FilePath"].ToString());

            var image = System.IO.File.OpenRead(model.ThumbPath);

            return File(image, "image/jpeg");
        }

        [HttpGet]
        public async Task<IActionResult> SetPayrollAccess()
        {
            return View("SetAccess");
        }

        [HttpPost]
        public async Task<IActionResult> SetAccess(SetAccessViewModel model)
        {
            await _iimportedRepository.SetPayrollAccessAsync(model.ProjectId, model.Year, model.Month, model.AccessType);

            _notify.Success("دسترسی ثبت شد.");

            return RedirectToAction("SetPayrollAccess");
        }

        [HttpGet]
        public async Task<IActionResult> TransferPersonnel()
        {
            return View("TransferPersonnel");
        }

        [HttpPost]
        public async Task<IActionResult> TransferPersonnel(TransferPersonnelViewModel model)
        {
            if(model.OldProjectId == model.NewProjectId)
            {
                _notify.Error("هر دو پروژه نمی تواند یکی باشد.");

                return RedirectToAction("TransferPersonnel");
            }

            await _iimportedRepository.TransferPersonnel(model.OldProjectId, model.NewProjectId);

            if (model.DisableOldProject)
            {
                var oldProject = await _projectRepository.GetByIdAsync(model.OldProjectId);

                oldProject.EndDate = DateTime.Now;

                oldProject.ProjectStatus = Common.Enums.ProjectStatus.Ended;

                await _projectRepository.UpdateAsync(oldProject);

                await _projectRepository.SaveChangesAsync();
            }

            _notify.Success("انتقال انجام شد.");

            return RedirectToAction("TransferPersonnel");
        }
    }
}