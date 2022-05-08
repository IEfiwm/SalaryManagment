using Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Abstractions;

namespace Web.Areas.Export.Controllers
{
    [Area("Export")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class ProjectStatusController : BaseController<InsuranceController>
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Excel(int year, int month, int projectId)
        {
            var permission = await _permissionCommon.CheckProjectPermissionByProjectId("ExportPorjectSummaryExcel", User, projectId);
            if (!permission)
            {
                _notify.Error(_localizer["AccessDeniedProject"].Value);
                return Ok();
            }

            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".xlsx", @$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Project/GetStatusReport/{year}/{month}/{projectId}");

            return File(viewModel.FileStream, "application/octet-stream", viewModel?.DownloadedFileName);
        }

        [HttpPost]
        public async Task<IActionResult> PDF(int year, int month, int projectId)
        {
            var permission = await _permissionCommon.CheckProjectPermissionByProjectId("ExportPorjectSummaryPDF", User, projectId);
            if (!permission)
            {
                _notify.Error(_localizer["AccessDeniedProject"].Value);
                return Ok();
            }
            
            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".pdf", @$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/MySummary/{year}/{month}/{projectId}");

            return File(viewModel.FileStream, "application/octet-stream", viewModel?.DownloadedFileName.Replace("\"", ""));
        }
    }
}
