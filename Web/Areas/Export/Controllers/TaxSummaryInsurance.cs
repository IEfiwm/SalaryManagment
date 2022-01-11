using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Web.Abstractions;

namespace Web.Areas.Export.Controllers
{
    [Area("Export")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class TaxSummaryInsurance : BaseController<TaxController>
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TXT(int year, int month, List<string> projectList)
        {
            var projectId = string.Join(',', projectList);
            return Redirect(@$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/TXTTaxSummary/{year}/{month}/{projectId}");
        }

        [HttpGet]
        public IActionResult PDF(int year, int month, List<string> projectList)
        {
            var projectId = string.Join(',', projectList);
            return Redirect(@$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/TaxSummary/{year}/{month}/{projectId}");
        }
    }
}
