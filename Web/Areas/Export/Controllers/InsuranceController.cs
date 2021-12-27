using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Abstractions;
using Web.Controllers;

namespace Web.Areas.Export.Controllers
{
    [Area("Export")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class InsuranceController : BaseController<InsuranceController>
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult DBF(int year, int month, long projectId)
        {
            return Redirect(@$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/DBFAll/{year}/{month}/{projectId}");
        }

        [HttpPost]
        public IActionResult PDF(int year, int month, long projectId)
        {
            return Redirect(@$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/InsuranceAll/{year}/{month}/{projectId}");
        }
    }
}
