using Web.Abstractions;
using Web.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Infrastructure.Repositories.Application.Basic;
using System.Collections.Generic;
using System.Linq;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class PermissionsController : BaseController<PermissionsController>
    {
        private readonly IPermissionRepository _permissionRepository;

        public PermissionsController(IPermissionRepository PermissionRepository)
        {
            _permissionRepository = PermissionRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll()
        {
            var model = _mapper.Map<List<PermissionsViewModel>>(await _permissionRepository.GetListAsync());

            return PartialView("_ViewAll", model);
        }

        public async Task<IActionResult> Create()
        {
            ViewData["PermissionList"] = _mapper.Map<List<PermissionsViewModel>>(await _permissionRepository.GetListAsync());
            return View();
        }

        public async Task<IActionResult> Edit(long PermissionId)
        {
            var model = await _permissionRepository.GetByIdAsync(PermissionId);

            var PermissionList = _mapper.Map<List<PermissionsViewModel>>(await _permissionRepository.GetListAsync());

            ViewData["PermissionList"] = PermissionList.Where(x => x.Id != PermissionId).ToList();

            return View(_mapper.Map<PermissionsViewModel>(model));
        }

        [HttpPost]
        public async Task<IActionResult> Create(PermissionsViewModel model)
        {
            var res = await _permissionRepository.InsertAndSaveAsync(_mapper.Map<Domain.Entities.Basic.Permission>(model));

            if (res > 0)
                _notify.Success("عملیات با موفقیت انجام شد.");
            else
                _notify.Error("عملیات با خطا مواجعه شد.");

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PermissionsViewModel model)
        {
            var modelAcc = _mapper.Map(model, await _permissionRepository.GetByIdAsync(model.Id));

            await _permissionRepository.UpdateAsync(modelAcc);

            var res = await _permissionRepository.SaveChangesAsync();

            if (res > 0)
                _notify.Success("عملیات با موفقیت انجام شد.");
            else
                _notify.Error("عملیات با خطا مواجعه شد.");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int PermissionId)
        {
            var isParent = await _permissionRepository.IsParent(PermissionId);

            if (isParent)
            {
                _notify.Error("امکان حذف وجود ندارد. رکورد انتخابی سرگروه می باشد");
                return RedirectToAction("Index");
            }

            var model = await _permissionRepository.GetByIdAsync(PermissionId);

            model.IsDeleted = true;

            await _permissionRepository.UpdateAsync(model);

            var res = await _permissionRepository.SaveChangesAsync();

            if (res > 0)
                _notify.Success("حذف با موفقیت انجام شد.");
            else
                _notify.Error("حذف انجام نشد.");

            return RedirectToAction("Index");
        }
    }
}