using Common.Enums;
using Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IActionResult> TXTWPAsync(int year, int month, PaymentType paymentMethod, List<string> projectList)
        {
            var projectId = string.Join(',', projectList);

            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".txt", @$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/TXTTaxWPAll/{year}/{month}/{projectId}");

            return File(viewModel.FileStream, "application/octet-stream", viewModel.DownloadedFileName);
        }

        [HttpPost]
        public async Task<IActionResult> TXTWHAsync(int year, int month, PaymentType paymentMethod, List<string> projectList)
        {
            var projectId = string.Join(',', projectList);

            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".txt", @$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/TXTTaxWHAll/{year}/{month}/{Convert.ToInt32(paymentMethod)}/{projectId}");

            return File(viewModel.FileStream, "application/octet-stream", viewModel.DownloadedFileName);
        }

        [HttpPost]
        public async Task<IActionResult> PDFAsync(int year, int month, List<string> projectList)
        {
            var projectId = string.Join(',', projectList);

            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".txt", @$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/TaxAll/{year}/{month}/{projectId}");

            return File(viewModel.FileStream, "application/octet-stream", viewModel.DownloadedFileName);
        }
    }
}
