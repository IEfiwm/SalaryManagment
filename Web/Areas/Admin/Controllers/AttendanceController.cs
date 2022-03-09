using Common.Models.DataTable;
using Infrastructure.Repositories.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class AttendanceController : BaseController<AttendanceController>
    {
        private readonly IimportedRepository _importedRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AttendanceController(IimportedRepository iimportedRepository,
            IHostingEnvironment hostingEnvironment)
        {
            _importedRepository = iimportedRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> Delete(long attendanceId)
        {
            var res = await _importedRepository.DeleteByIdAsync(attendanceId);

            if (res)
                _notify.Success("حذف کارکرد با موفقیت انجام شد.");
            else
                _notify.Error("حذف کارکرد انجام نشد.");

            return RedirectToAction("attendanceList");
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoadAll(int year, int month, long projectId, string key, byte pageSize, byte pageNumber)
        {
            var usersattendance = await _importedRepository.GetUserAttendanceListAsync(year, month, projectId, key, pageSize, pageNumber);

            var res = _mapper.Map<DataTableViewModel<IEnumerable<AttendanceViewModel>>>(usersattendance);

            return PartialView("_ViewAll", res);
        }


        public IActionResult GetTemplate()
        {

            string webRootPath = _hostingEnvironment.WebRootPath;

            string path = Path.Combine(webRootPath, "Files/Template/Karkard_temp.xlsx");

            var stream = new FileStream(path, FileMode.Open);

            if (stream == null)
                return NotFound();

            return File(stream, "application/octet-stream", "Karkard_temp.xlsx");
        }
    }
}