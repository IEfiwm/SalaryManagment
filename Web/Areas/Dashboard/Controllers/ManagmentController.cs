using Web.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Repositories.Application.Basic;
using Infrastructure.Repositories.Application;
using Infrastructure.Repositories.Application.Idenitity;
using Web.Areas.Dashboard.Models;
using System.Threading.Tasks;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ManagmentController : BaseController<ManagmentController>
    {
        private readonly IProjectRepository _projectRepository;

        private readonly IimportedRepository _iimportedRepository;

        private readonly IUserRepository _userRepository;

        public ManagmentController(IProjectRepository projectRepository,
            IimportedRepository iimportedRepository,
            IUserRepository userRepository)
        {
            _iimportedRepository = iimportedRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = new SummaryViewModel();

            model.ImportedDataCount = (await _iimportedRepository.GetListAsync()).Count;
            model.ProjectCount = (await _projectRepository.GetListAsync()).Count;
            model.UserCount = (await _userRepository.GetUserListAsync()).Count;

            _notify.Information("خوش آمدید !");

            return View(model);
        }
    }
}