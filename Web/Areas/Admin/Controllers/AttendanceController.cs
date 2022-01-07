using Application.Enums;
using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Infrastructure.Repositories.Application;
using Infrastructure.Repositories.Application.Basic;
using Infrastructure.Repositories.Application.Idenitity;
using MD.PersianDateTime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
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

        public AttendanceController(IimportedRepository iimportedRepository)
        {
            _importedRepository = iimportedRepository;
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

        public IActionResult AttendanceList()
        {
            var model = new List<AttendanceViewModel>();

            var usersattendance = _importedRepository.GetUserAttendanceList();
            model = _mapper.Map<IEnumerable<AttendanceViewModel>>(usersattendance).ToList();
            
            return View(model);

        }

    }
}