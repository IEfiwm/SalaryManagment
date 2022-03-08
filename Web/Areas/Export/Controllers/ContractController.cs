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
    public class ContractController : BaseController<ContractController>
    {
        [HttpGet("Export/Contract/Download/{nationalCode}/{projectId}/{startDate}/{endDate}")]
        public async Task<IActionResult> Download(string nationalCode, string projectId, string startDate,string endDate)
        {
            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".pdf", @$"{ConfigurationStorage.GetValue("Base:KoshaCore:APIAddress")}/Contract/Get/{nationalCode}/{projectId}/{startDate}/{endDate}");

            return File(viewModel.FileStream, "application/octet-stream", viewModel.DownloadedFileName);
        }
    }
}
