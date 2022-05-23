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
        public async Task<IActionResult> Download(string nationalCode, string projectId, string startDate, string endDate)
        {
            if (!string.IsNullOrEmpty(projectId))
            {
                var permission = await _permissionCommon.CheckProjectPermissionByProjectId("ShowContractList", User, Convert.ToInt64(projectId));
                if (!permission)
                {
                    _notify.Error(_localizer["AccessDeniedProject"].Value);
                    return Ok();
                }
            }


            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".pdf", @$"{ConfigurationStorage.GetValue("Base:KoshaCore:APIAddress")}/Contract/Get/{nationalCode}/{projectId}/{startDate}/{endDate}");

            if (viewModel is null)
            {
                _notify.Error("یافت نشد!");
                return Ok();
            }
            return File(viewModel?.FileStream, "application/octet-stream", viewModel?.DownloadedFileName);
        }
    }
}
