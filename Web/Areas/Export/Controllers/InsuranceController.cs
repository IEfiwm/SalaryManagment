using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult DBF(int year, int month)
        {
            return Redirect(@$"https://kosha.tj.ngraapp.ir/Report/DBFAll/{year}/{month}");
        }

        [HttpPost]
        public IActionResult PDF(int year, int month)
        {
            return Redirect(@$"https://kosha.tj.ngraapp.ir/Report/InsuranceAll/{year}/{month}");
        }
    }
}
