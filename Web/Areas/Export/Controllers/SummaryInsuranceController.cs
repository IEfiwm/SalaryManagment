using Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Abstractions;

namespace Web.Areas.Export.Controllers
{
    [Area("Export")]
    [Authorize]
    public class SummaryInsuranceController : BaseController<InsuranceController>
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> DBFAsync(int year, int month, int projectId)
        {
            var permission = await _permissionCommon.CheckProjectPermissionByProjectId("ExportInsuranceSummary", User, projectId);
            if (!permission)
            {
                _notify.Error(_localizer["AccessDeniedProject"].Value);
                return Ok();
            }

            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".txt", @$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/DBFSummary/{year}/{month}/{projectId}");

            return File(viewModel.FileStream, "application/octet-stream", viewModel.DownloadedFileName);
        }

        [HttpGet]
        public async Task<IActionResult> PDFAsync(int year, int month, int projectId)
        {
            var permission = await _permissionCommon.CheckProjectPermissionByProjectId("ExportInsuranceSummary", User, projectId);
            if (!permission)
            {
                _notify.Error(_localizer["AccessDeniedProject"].Value);
                return Ok();
            }
            
            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".pdf", @$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/InsuranceSummary/{year}/{month}/{projectId}");

            return File(viewModel.FileStream, "application/octet-stream", viewModel.DownloadedFileName.Replace("\"", ""));
        }
    }
}
