using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Abstractions;
using Web.Controllers;

namespace Web.Areas.Export.Controllers
{
    [Area("Export")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class BankController : BaseController<BankController>
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TXT(int year, int month, long projectId)
        {
            return Redirect(@$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/BankTXT/{year}/{month}/{projectId}");
        }
        [HttpPost]
        public IActionResult PDF(int year, int month, long projectId)
        {
            return Redirect(@$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/BankPDF/{year}/{month}/{projectId}");
        }

        
    }
}
