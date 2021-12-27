using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Abstractions;

namespace Web.Areas.Export.Controllers
{
    [Area("Export")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class SummaryInsurance : BaseController<InsuranceController>
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult DBF(int year, int month)
        {
            return Redirect(@$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/DBFSummary/{year}/{month}");
        }

        public IActionResult PDF(int year, int month)
        {
            return Redirect(@$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/InsuranceSummary/{year}/{month}");
        }
    }
}
