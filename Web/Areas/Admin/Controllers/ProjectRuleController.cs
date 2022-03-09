using Domain.Entities.Basic;
using Infrastructure.Repositories.Application.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class ProjectRuleController : BaseController<UserController>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectRuleRepository _projectRuleRepository;

        public ProjectRuleController(
            IProjectRepository projectRepository,
            IProjectRuleRepository projectRuleRepository)
        {
            _projectRepository = projectRepository;
            _projectRuleRepository = projectRuleRepository;
        }

        public async Task<IActionResult> Index(long? projectId = null)
        {
            var projects = await _projectRepository.GetListAsync();
            ViewData["projects"] = _mapper.Map<List<ProjectViewModel>>(projects);
            ViewData["projectId"] = projectId;
            return View();
        }

        public async Task<IActionResult> Create(long? projectId = null)
        {
            JsonSerializer serializer = new JsonSerializer();
            string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            var calculatedByfile = System.IO.File.ReadAllText(solutiondir + "\\" + @"\SalaryManagment\Infrastructure\Json\CalculateByProps.json");
            var calculatedfile = System.IO.File.ReadAllText(solutiondir + "\\" + @"\SalaryManagment\Infrastructure\Json\CalculatedProps.json");

            var jObject = JObject.Parse(calculatedByfile);
            ViewData["CalculatedByProps"] = JArray.Parse(jObject["props"].ToString()).Select(x => (string)x.Value<string>()).ToList();

            jObject = JObject.Parse(calculatedfile);
            ViewData["CalculatedProps"] = JArray.Parse(jObject["props"].ToString()).Select(x => (string)x.Value<string>()).ToList();

            var projects = await _projectRepository.GetListAsync();
            ViewData["projects"] = _mapper.Map<List<ProjectViewModel>>(projects);


            var model = new ProjectRuleViewModel();
            if (projectId != null)
                model.ProjectId = projectId.Value;

            return View(model);
        }



        public async Task<IActionResult> LoadAll(long? projectId = null)
        {
            var projects = await _projectRepository.GetListAsync();
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
            string solutiondir = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

            var calculatedByfile = System.IO.File.ReadAllText(solutiondir + "\\" + @"\SalaryManagment\Infrastructure\Json\CalculateByProps.json");
            var calculatedfile = System.IO.File.ReadAllText(solutiondir + "\\" + @"\SalaryManagment\Infrastructure\Json\CalculatedProps.json");

            var jObject = JObject.Parse(calculatedByfile);
            ViewData["CalculatedByProps"] = JArray.Parse(jObject["props"].ToString()).Select(x => (string)x.Value<string>()).ToList();

            jObject = JObject.Parse(calculatedfile);
            ViewData["CalculatedProps"] = JArray.Parse(jObject["props"].ToString()).Select(x => (string)x.Value<string>()).ToList();

            var projects = await _projectRepository.GetListAsync();
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


                if (model.Id == 0)
                {
                    ruleModel.Rule = String.Join(" & ", model.RuleList);
                    await _projectRuleRepository.InsertAsync(ruleModel);
                }
                else
                {
                    ruleModel = await _projectRuleRepository.GetByIdAsync(Convert.ToInt32(model.Id));
                    _mapper.Map(model, ruleModel);

                    ruleModel.Rule = String.Join(" & ", model.RuleList);

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