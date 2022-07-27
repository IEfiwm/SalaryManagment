using Web.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Repositories.Application.Basic;
using Infrastructure.Repositories.Application;
using Infrastructure.Repositories.Application.Idenitity;
using Web.Areas.Dashboard.Models;
using System.Threading.Tasks;
using Infrastructure.Base.Permission;
using Common.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ManagmentController : BaseController<ManagmentController>
    {
        private readonly IProjectRepository _projectRepository;

        private readonly IimportedRepository _iimportedRepository;

        private readonly IUserRepository _userRepository;

        private readonly IPermissionCommon _permissionCommon;

        private readonly IWebHostEnvironment _env;

        public ManagmentController(IProjectRepository projectRepository,
            IimportedRepository iimportedRepository,
            IUserRepository userRepository,
            IPermissionCommon permissionCommon,
            IWebHostEnvironment env)
        {
            _iimportedRepository = iimportedRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
            _permissionCommon = permissionCommon;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var model = new SummaryViewModel();

            if (!_env.IsDevelopment())
            {
                model.ImportedDataCount = (await _iimportedRepository.GetListAsync()).Count;

                model.ProjectCount = (await _permissionCommon.GetProjectsByPermission("ShowDoshboard", HttpContext.User)).Count;

                model.UserCount = (await _userRepository.GetUserListAsync()).Count;
            }

            _notify.Information("خوش آمدید !");

            return View(model);
        }
    }
}