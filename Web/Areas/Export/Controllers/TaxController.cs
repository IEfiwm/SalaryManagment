using Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Web.Abstractions;
using Web.Controllers;

namespace Web.Areas.Export.Controllers
{
    [Area("Export")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class TaxController : BaseController<TaxController>
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult TXTWP(int year, int month, PaymentType paymentMethod, List<string> projectList)
        {
            var projectId = string.Join(',', projectList);
            return Redirect(@$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/TXTTaxWPAll/{year}/{month}/{projectId}");
        }

        [HttpPost]
        public IActionResult TXTWH(int year, int month, PaymentType paymentMethod, List<string> projectList)
        {
            var projectId = string.Join(',', projectList);
            return Redirect(@$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/TXTTaxWHAll/{year}/{month}/{Convert.ToInt32(paymentMethod)}/{projectId}");
        }

        [HttpPost]
        public IActionResult PDF(int year, int month, List<string> projectList)
        {
            var projectId = string.Join(',', projectList);
            return Redirect(@$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/TaxAll/{year}/{month}/{projectId}");
        }
    }
}
