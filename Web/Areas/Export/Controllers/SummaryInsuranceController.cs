using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Abstractions;

namespace Web.Areas.Export.Controllers
{
    [Area("Export")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class SummaryInsuranceController : BaseController<InsuranceController>
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DBF(int year, int month, int projectId)
        {
            return Redirect(@$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/DBFSummary/{year}/{month}/{projectId}");
        }

        [HttpGet]
        public IActionResult PDF(int year, int month, int projectId)
        {
            return Redirect(@$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/InsuranceSummary/{year}/{month}/{projectId}");
        }
    }
}
