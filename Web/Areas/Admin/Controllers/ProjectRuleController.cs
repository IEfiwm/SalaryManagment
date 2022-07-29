using Domain.Entities.Basic;
using Infrastructure.Base.Permission;
using Infrastructure.Repositories.Application.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
    public class ProjectRuleController : BaseController<UserController>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectRuleRepository _projectRuleRepository;
        private readonly IFieldRepository _fieldRepository;
        private readonly IPermissionCommon _permissionCommon;

        public ProjectRuleController(
            IProjectRepository projectRepository,
            IProjectRuleRepository projectRuleRepository,
            IFieldRepository fieldRepository,
            IPermissionCommon permissionCommon)
        {
            _projectRepository = projectRepository;
            _projectRuleRepository = projectRuleRepository;
            _fieldRepository = fieldRepository;
            _permissionCommon = permissionCommon;
        }

        public async Task<IActionResult> Index(long? projectId = null)
        {
            var projects = await _permissionCommon.GetProjectsByPermission("ShowProjectRule", HttpContext.User);
            ViewData["projects"] = _mapper.Map<List<ProjectViewModel>>(projects);
            ViewData["projectId"] = projectId;
            return View();
        }

        public async Task<IActionResult> Create(long? projectId = null)
        {
            var calculatedByfile = await _fieldRepository.GetCalculatedByFields();

            ViewData["CalculatedByProps"] = _mapper.Map<List<FieldViewModel>>(calculatedByfile);

            var calculatedfile = await _fieldRepository.GetCalculateFields();

            ViewData["CalculatedProps"] = _mapper.Map<List<FieldViewModel>>(calculatedfile);

            var projects = await _permissionCommon.GetProjectsByPermission("CreateProjectRule", HttpContext.User);

            ViewData["projects"] = _mapper.Map<List<ProjectViewModel>>(projects);

            var model = new ProjectRuleViewModel();

            if (projectId != null)
                model.ProjectId = projectId.Value;

            return View(model);
        }

        public async Task<IActionResult> LoadAll(long? projectId = null)
        {
            var projects = await _permissionCommon.GetProjectsByPermission("ShowProjectRule", HttpContext.User);
            ViewData["projects"] = _mapper.Map<List<ProjectViewModel>>(projects);

            var projectList = await _projectRuleRepository.GetListAsync();

            if (projectId != null)
                projectList = projectList.Where(x => x.ProjectId == projectId.Value).ToList();

            var model = _mapper.Map<IEnumerable<ProjectRuleViewModel>>(projectList);

            return PartialView("_ViewAll", model);
        }

        public async Task<IActionResult> Edit(int projectRuleId)
        {

            JsonSerializer serializer = new JsonSerializer();


            var calculatedByfile = await _fieldRepository.GetCalculatedByFields();
            ViewData["CalculatedByProps"] = _mapper.Map<List<FieldViewModel>>(calculatedByfile);

            var calculatedfile = await _fieldRepository.GetCalculateFields();
            ViewData["CalculatedProps"] = _mapper.Map<List<FieldViewModel>>(calculatedfile);

            var projects = await _permissionCommon.GetProjectsByPermission("EditProjectRule", HttpContext.User);
            ViewData["projects"] = _mapper.Map<List<ProjectViewModel>>(projects);

            var model = await _projectRuleRepository.GetByIdAsync(projectRuleId);
            var viewModel = _mapper.Map<ProjectRuleViewModel>(model);

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProjectRuleViewModel model)
        {
            try
            {

                ProjectRule ruleModel = _mapper.Map<ProjectRule>(model);

                var field = await _fieldRepository.GetByIdAsync(model.FieldId);

                ruleModel.Name = field.Alias;


                if (model.Id == 0)
                {
                    var permission = await _permissionCommon.CheckProjectPermissionByProjectId("CreateProjectRule", User, model.ProjectId);
                    if (!permission)
                    {
                        _notify.Error(_localizer["AccessDeniedProject"].Value);
                        return RedirectToAction("Index");
                    }

                    if (model.RuleList == null || model.RuleList?.Count == 0)
                    {
                        _notify.Warning("لطفا فرمول را پر کنید.");
                        return RedirectToAction("create");
                    }

                    var dup = await _projectRuleRepository.GetByFieldtId(model.FieldId, model.ProjectId);
                    if (dup)
                    {
                        _notify.Warning(ruleModel.Name + " برای پروژه مورد نظر تعریف شده و تکراری می باشد.");
                        return RedirectToAction("Create");
                    }

                    ruleModel.Rule = String.Join(" & ", model.RuleList);
                    await _projectRuleRepository.InsertAsync(ruleModel);
                }
                else
                {
                    var permission = await _permissionCommon.CheckProjectPermissionByProjectId("EditProjectRule", User, model.ProjectId);
                    if (!permission)
                    {
                        _notify.Error(_localizer["AccessDeniedProject"].Value);
                        return RedirectToAction("Index");
                    }

                    if (model.RuleList == null || model.RuleList?.Count == 0)
                    {
                        _notify.Warning("لطفا فرمول را پر کنید.");
                        return RedirectToAction("Edit", new { projectRuleId = model.Id });
                    }

                    var dup = await _projectRuleRepository.GetByFieldtId(model.FieldId, model.ProjectId, model.Id);
                    if (dup)
                    {
                        _notify.Warning(ruleModel.Name + " برای پروژه مورد نظر تعریف شده و تکراری می باشد.");
                        return RedirectToAction("Edit", new { projectRuleId = model.Id });
                    }

                    ruleModel = await _projectRuleRepository.GetByIdAsync(Convert.ToInt32(model.Id));
                    _mapper.Map(model, ruleModel);

                    ruleModel.Rule = String.Join(" & ", model.RuleList);
                    ruleModel.Name = field.Alias;

                }

                var res = await _projectRuleRepository.SaveChangesAsync();


                _notify.Success("عملیات با موفقیت انجام شد.");



            }
            catch (Exception x)
            {

                _notify.Error("عملیات با خطا مواجعه شد.");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int projectRuleId)
        {
            var model = await _projectRuleRepository.GetByIdAsync(projectRuleId);

            model.IsDeleted = true;

            await _projectRuleRepository.UpdateAsync(model);

            var res = await _projectRuleRepository.SaveChangesAsync();

            if (res > 0)
                _notify.Success("حذف با موفقیت انجام شد.");
            else
                _notify.Error("حذف انجام نشد.");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var model = await _projectRuleRepository.GetListAsync();

            return Json(model);
        }
    }
}