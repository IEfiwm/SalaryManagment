using Common.Models.DataTable;
using Infrastructure.Base.Permission;
using Infrastructure.Repositories.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Areas.Admin.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AttendanceController : BaseController<AttendanceController>
    {
        private readonly IimportedRepository _importedRepository;
        private readonly IPermissionCommon _permissionCommon;

        public AttendanceController(IimportedRepository iimportedRepository,
            IPermissionCommon permissionCommon)
        {
            _importedRepository = iimportedRepository;
            _permissionCommon = permissionCommon;
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
            var permission = await _permissionCommon.CheckProjectPermissionByProjectId("ShowAttendance", User, projectId);

            if (!permission)
            {
                _notify.Error(_localizer["AccessDeniedProject"].Value);
                return Ok();
            }

            var usersattendance = await _importedRepository.GetUserAttendanceListAsync(year, month, projectId, key, pageSize, pageNumber);

            var res = _mapper.Map<DataTableViewModel<IEnumerable<AttendanceViewModel>>>(usersattendance);

            return PartialView("_ViewAll", res);
        }

        public async Task<IActionResult> ExportExcel(int year, int month, long projectId, string key, byte pageSize, byte pageNumber)
        {
            var permission = await _permissionCommon.CheckProjectPermissionByProjectId("ShowAttendance", User, projectId);

            if (!permission)
            {
                _notify.Error(_localizer["AccessDeniedProject"].Value);

                return Ok();
            }

            var usersattendance = await _importedRepository.GetUserAttendanceListAsync(year, month, projectId, key, int.MaxValue, pageNumber);

            var res = _mapper.Map<DataTableViewModel<IEnumerable<AttendanceViewModel>>>(usersattendance);

            MemoryStream result = (MemoryStream)ExportToExcel(res.ViewModel);
            // Set memorystream position; if we don't it'll fail
            result.Position = 0;

            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Attendances.xlsx");
        }

        private object ExportToExcel(IEnumerable<AttendanceViewModel> model)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excelFile = new ExcelPackage())
            {
                var NewModel = model.Select(x => new
                {
                    x.PersonnelCode,
                    x.FirstName,
                    x.LastName,
                    x.JobTitle,
                    x.BirthPlace,
                    x.AccountNumber,
                    x.NationalCode,
                    x.InsuranceNumber,
                    x.ServiceLocation,
                    x.DegreeOfEducation,
                    x.DurationOperation,
                    x.OvertimeworkingTime,
                    x.NightworkingTime,
                    x.HolidayworkingTime,
                    x.MissionTime,
                    x.FoodTime,
                    x.NumberChildren,
                    x.Salary,
                    x.SeveranceDaily,//سنوات پایه روزانه 
                    x.DailyPay,
                    x.FoodAndHousingRight,
                    x.WorkerRight,
                    x.OvertimePay,
                    x.MonthlyPay,// سنوات
                    x.OvertimeworkingPay,
                    x.ChildrenRightPay,
                    x.HouseRightPay,
                    x.WorkerRightPay,
                    x.NightworkingPay,
                    x.HolidayworkingPay,
                    x.MissionPay,
                    x.FoodPay,
                    x.Other01,
                    x.Other02,
                    x.Disparity,
                    x.PreviousReceipt,
                    x.SumSalaryAndBenefit,
                    x.TaxationPay,
                    x.InsurancePay,
                    x.Insurance7Percent,
                    x.Taxation,
                    x.HelpPay,
                    x.Absence,
                    x.Debt,
                    x.OtherDeductions,
                    x.SumDeductions,
                    x.PureIncome,
                    x.Year,
                    x.Month,
                    x.SeveranceMonthly,//پایه سنوات مزایا
                    x.ShiftWorkTime,
                    x.ShiftWorkPay,
                    x.SupplementaryInsuranceDeduction,
                    x.RewardTime,
                    x.YearsPay,//سنوات-مزایا 
                    x.RewardPay,
                    x.FestalPay,
                    x.BasicOverTimePay,
                    x.SupplementaryInsuranceSupervisor,
                    x.SupplementaryInsuranceForDependents,
                    x.NonDependentSupplementaryInsurance,
                    x.WelfareCostPay,
                    x.TransportationPay,
                    x.DelayedTime,
                    x.ClothesPay,
                    x.InstitutionaLoan,
                    x.SamanLoan,
                    x.DelayedTransportationPay,
                    x.DelayedSupplementaryInsuranceDeduction,
                    x.WelfareAllowancePay,
                    x.PerformancePay,
                    x.MonthlyBenefits,
                    x.MonthlyWagesAndBenefitsIncluded,
                    x.IncludedAndNotIncluded,
                    x.UnemploymentInsurance,
                    x.Insurance30Percent,
                    x.EmployerShareInsurance,
                    x.ContinuousBasicRightsToHousingAndChildrenRights,
                    x.ContinuousBaseSalaryAndBaseYears,
                    x.NonContinuousIncludedNotIncluded,
                    x.NonContinuousIncluded,
                });

                var worksheet = excelFile.Workbook.Worksheets.Add("Personnel");
                worksheet.View.RightToLeft = true;
                worksheet.Cells["A1"].Value = _localizer["PersonnelCode"].Value;
                worksheet.Cells["B1"].Value = _localizer["FirstName"].Value  ;
                worksheet.Cells["C1"].Value = _localizer["LastName"].Value;
                worksheet.Cells["D1"].Value = _localizer["JobTitle"].Value;
                worksheet.Cells["E1"].Value = _localizer["BirthPlace"].Value;
                worksheet.Cells["F1"].Value = "شماره حساب";
                worksheet.Cells["G1"].Value = _localizer["NationalCode"].Value;
                worksheet.Cells["H1"].Value = _localizer["InsuranceCode"].Value;
                worksheet.Cells["I1"].Value = "محل خدمت";
                worksheet.Cells["J1"].Value = _localizer["DegreeOfEducation"].Value;
                worksheet.Cells["K1"].Value = "مدت خدمت";
                worksheet.Cells["L1"].Value = "کارکرد اضافه کاری";
                worksheet.Cells["M1"].Value = "کارکرد شبکاری";
                worksheet.Cells["N1"].Value = "کارکرد تعطیل کاری";
                worksheet.Cells["O1"].Value = "کارکرد ماموریت ";
                worksheet.Cells["P1"].Value = "كاركرد حق غذا ";
                worksheet.Cells["Q1"].Value = "تعداد اولاد";
                worksheet.Cells["R1"].Value = "حقوق پایه";
                worksheet.Cells["S1"].Value = "پایه سنوات روزانه";// SeveranceDaily
                worksheet.Cells["T1"].Value = "مزد روزانه";
                worksheet.Cells["U1"].Value = _localizer["FoodAndHouseRight"].Value;
                worksheet.Cells["V1"].Value = "بن کارگر";
                worksheet.Cells["W1"].Value = "مبلغ ساعت اضافه کاری";
                worksheet.Cells["X1"].Value = "دستمزد ماهیانه"; //MonthlyPay
                worksheet.Cells["Y1"].Value = "مبلغ اضافه کاری";
                worksheet.Cells["Z1"].Value = "حق اولاد";
                worksheet.Cells["AA1"].Value = "حق مسکن" ;
                worksheet.Cells["AB1"].Value = "حق بن کارگری";
                worksheet.Cells["AC1"].Value = "شب کاری";
                worksheet.Cells["AD1"].Value = "تعطیل کاری";
                worksheet.Cells["AE1"].Value = "ماموریت";
                worksheet.Cells["AF1"].Value = "هزینه غذا";
                worksheet.Cells["AG1"].Value = "سایر1";
                worksheet.Cells["AH1"].Value = "سایر2";
                worksheet.Cells["AI1"].Value = "مابه التفاوت";
                worksheet.Cells["AJ1"].Value = "طلب از قبل";
                worksheet.Cells["AK1"].Value = "جمع حقوق و مزایا";
                worksheet.Cells["AL1"].Value = "مشمول مالیات";
                worksheet.Cells["AM1"].Value = "مشمول بیمه";
                worksheet.Cells["AN1"].Value = "بیمه 7%";
                worksheet.Cells["AO1"].Value = "ماليات";
                worksheet.Cells["AP1"].Value = "مساعده";
                worksheet.Cells["AQ1"].Value = "غیبت";
                worksheet.Cells["AR1"].Value = "بدهی از قبل";
                worksheet.Cells["AS1"].Value = "سایر کسورات";
                worksheet.Cells["AT1"].Value = "جمع كسورات";
                worksheet.Cells["AU1"].Value = "خالص دریافتی";
                worksheet.Cells["AV1"].Value = "سال";
                worksheet.Cells["AW1"].Value = "ماه";
                worksheet.Cells["AX1"].Value = "پایه سنوات مزایا";//SeveranceMonthly 
                worksheet.Cells["AY1"].Value = "نوبت کاری-کارکرد";
                worksheet.Cells["AZ1"].Value = "نوبتکاری -مزایا";
                worksheet.Cells["BA1"].Value = "کسر بیمه تکمیلی - کسورات";
                worksheet.Cells["BB1"].Value = "پاداش - کارکرد";
                worksheet.Cells["BC1"].Value = "سنوات-مزایا ";
                worksheet.Cells["BD1"].Value = "عیدی-مزایا";
                worksheet.Cells["BE1"].Value = "پاداش - مزایا";
                worksheet.Cells["BF1"].Value = "اضافه کاری ثابت-مزایا";
                worksheet.Cells["BG1"].Value = "کسر بیمه تکمیلی سرپرست";
                worksheet.Cells["BH1"].Value = "کسر بیمه تکمیلی افراد تحت تکفل";
                worksheet.Cells["BI1"].Value = "کسر بیمه تکمیلی افرادغیر تحت تکفل";
                worksheet.Cells["BJ1"].Value = "هزینه رفاهی";
                worksheet.Cells["BK1"].Value = "ایاب و ذهاب";
                worksheet.Cells["BL1"].Value = "کارکرد معوقه";
                worksheet.Cells["BM1"].Value = "هزینه لباس";
                worksheet.Cells["BN1"].Value = "وام موسسه";
                worksheet.Cells["BO1"].Value = "وام سامان";
                worksheet.Cells["BP1"].Value = "معوقه ایاب و ذهاب";
                worksheet.Cells["BQ1"].Value = "کسر بیمه تکمیلی ماه معوقه";
                worksheet.Cells["BR1"].Value = "کمک هزینه رفاهی";
                worksheet.Cells["BS1"].Value = "کارایی";
                worksheet.Cells["BT1"].Value = "مزایای مشمول";
                worksheet.Cells["BU1"].Value = "دستمزد و مزایای مشمول ماهانه";
                worksheet.Cells["BV1"].Value = "مشمول و غیر مشمول";
                worksheet.Cells["BW1"].Value = "بیمه بیکاری";
                worksheet.Cells["BX1"].Value = "بیمه 30%";
                worksheet.Cells["BY1"].Value = "بیمه سهم کارفرما";
                worksheet.Cells["BZ1"].Value = "مستمر - حقوق پایه بن و مسکن و حق اولاد";
                worksheet.Cells["CA1"].Value = "مستمر - حقوق پایه و پایه سنوات";
                worksheet.Cells["CB1"].Value = "غیر مستمر - مشمول و غیر مشمول";
                worksheet.Cells["CC1"].Value = "غیر مستمر - مشمول";


                worksheet.Cells["A2"].LoadFromCollection(Collection: NewModel, PrintHeaders: false, OfficeOpenXml.Table.TableStyles.Light13);

                var allCells = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];

                var cellFont = allCells.Style.Font;

                //cellFont.SetFromFont("B Nazanin", 11);

                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                MemoryStream result = new MemoryStream();

                result.Position = 0;

                excelFile.SaveAs(result);

                return result;
            }
        }
    }
}