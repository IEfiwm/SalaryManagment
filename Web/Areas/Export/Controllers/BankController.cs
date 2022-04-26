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
    public class BankController : BaseController<BankController>
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> TXTAsync(int year, int month, long projectId)
        {
            var permission = await _permissionCommon.CheckProjectPermissionByProjectId("ExportBank", User, projectId);
            if (!permission)
            {
                _notify.Error(_localizer["AccessDeniedProject"].Value);
                return Ok();
            }
            
            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".txt", @$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/BankTXT/{year}/{month}/{projectId}");

            return File(viewModel.FileStream, "application/octet-stream", viewModel.DownloadedFileName.Replace("\"", ""));
        }

        [HttpPost]
        public async Task<IActionResult> PDFAsync(int year, int month, long projectId)
        {
            var permission = await _permissionCommon.CheckProjectPermissionByProjectId("ExportBank", User, projectId);
            if (!permission)
            {
                _notify.Error(_localizer["AccessDeniedProject"].Value);
                return Ok();
            }
            
            var viewModel = await new FileHelper().DownloadAndReturnMemorySreamAsync(Guid.NewGuid() + ".txt", @$"{_configuration["Base:KoshaCore:APIAddress"].ToString()}/Report/BankPDF/{year}/{month}/{projectId}");

            return File(viewModel.FileStream, "application/octet-stream", viewModel.DownloadedFileName.Replace("\"",""));
        }
    }
}
