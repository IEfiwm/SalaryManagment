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
        [HttpGet("Export/PayRoll/Download/{nationalCode}/{bankAccNumber}/{year}/{month}/{projectId}")]
        public async Task<IActionResult> Download(string nationalCode, string bankAccNumber, string year, string month,long projectId)
        {
            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".pdf", @$"{ConfigurationStorage.GetValue("Base:KoshaCore:APIAddress")}/PayRoll/GetPayRoll/{nationalCode}/{bankAccNumber}/{year}/{month}");
            if (viewModel is null)
            {
                _notify.Error("فیش حقوقی یافت نشد.");
                return RedirectToAction("PayRollTipList", "User", new {area="Admin", year = year, month = month, projectId= projectId });
            }
            return File(viewModel?.FileStream, "application/octet-stream", viewModel?.DownloadedFileName);
        }
    }
}
