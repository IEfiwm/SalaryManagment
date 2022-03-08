using Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Models;

namespace Web.Areas.Export.Controllers
{
    [Area("Export")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class PayRollController : BaseController<PayRollController>
    {
        [HttpGet("Export/PayRoll/Download/{nationalCode}/{bankAccNumber}/{year}/{month}")]
        public async Task<IActionResult> Download(string nationalCode, string bankAccNumber, string year, string month)
        {
            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".pdf", @$"{ConfigurationStorage.GetValue("Base:KoshaCore:APIAddress")}/PayRoll/GetPayRoll/{nationalCode}/{bankAccNumber}/{year}/{month}");

            return File(viewModel.FileStream, "application/octet-stream", viewModel.DownloadedFileName);
        }
    }
}
