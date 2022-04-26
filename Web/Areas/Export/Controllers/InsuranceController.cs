using Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Controllers;

namespace Web.Areas.Export.Controllers
{
    [Area("Export")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class InsuranceController : BaseController<InsuranceController>
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DBFAsync(int year, int month, long projectId)
        {
            var permission = await _permissionCommon.CheckProjectPermissionByProjectId("ExportInsuranceList", User, projectId);
            if (!permission)
            {
                _notify.Error(_localizer["AccessDeniedProject"].Value);
                return Ok();
            }
            
            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".txt", @$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/DBFAll/{year}/{month}/{projectId}");

            return File(viewModel.FileStream, "application/octet-stream", viewModel.DownloadedFileName);
        }

        [HttpPost]
        public async Task<IActionResult> PDFAsync(int year, int month, long projectId)
        {
            var permission = await _permissionCommon.CheckProjectPermissionByProjectId("ExportInsuranceList", User, projectId);
            if (!permission)
            {
                _notify.Error(_localizer["AccessDeniedProject"].Value);
                return Ok();
            }
            
            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".pdf", @$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/InsuranceAll/{year}/{month}/{projectId}");

            return File(viewModel.FileStream, "application/octet-stream", viewModel.DownloadedFileName.Replace("\"",""));
        }
    }
}
