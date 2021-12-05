using Web.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Web.Areas.Dashboard.Controllers
{
    [Area("Dashboard")]
    public class ManagmentController : BaseController<ManagmentController>
    {
        public IActionResult Index()
        {
            _notify.Information("خوش آمدید !");
            
            return View();
        }
    }
}   