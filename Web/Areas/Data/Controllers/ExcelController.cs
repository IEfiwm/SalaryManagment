using Application.Enums;
using Application.Extensions;
using Common.Enums;
using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Domain.Entities.Data;
using Infrastructure.Repositories.Application;
using Infrastructure.Repositories.Application.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Controllers;

namespace Web.Areas.Attendance.Controllers
{
    [Area("Data")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class ExcelController : BaseController<Imported>
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IimportedRepository _repository;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IBankAccountRepository _bankAccountRepository;

        private readonly IProjectRepository _projectRepository;

        public ExcelController(IHostingEnvironment hostingEnvironment,
            IimportedRepository repository,
            UserManager<ApplicationUser> userManager,
            IBankAccountRepository bankAccountRepository,
            IProjectRepository projectRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _repository = repository;
            _userManager = userManager;
            _bankAccountRepository = bankAccountRepository;
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public IActionResult Attendances()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Personnel()
        {
            return View();
        }

        [HttpPost]
        public async Task<bool> ImportPersonnel()
        {
            var personnelCode = "";

            int count = 0;

            try
            {
                IFormFile file = Request.Form.Files[0];

                string folderName = "UploadExcel";

                string webRootPath = _hostingEnvironment.WebRootPath;

                string newPath = Path.Combine(webRootPath, folderName);

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                if (file.Length > 0)
                {
                    string sFileExtension = Path.GetExtension(file.FileName).ToLower();

                    ISheet sheet;

                    string fullPath = Path.Combine(newPath, file.FileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);

                        stream.Position = 0;

                        if (sFileExtension == ".xls")
                        {
                            HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  

                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                        }

                        else
                        {
                            XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  

                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                        }

                        _notify.Success("اطلاعات دریافت شدند، سامانه در حال وارد کردن اطلاعات است.");

                        for (int j = 1; j < sheet.LastRowNum + 1; j++)
                        {
                            var row = sheet.GetRow(j);

                            personnelCode = row?.GetCell(1)?.ToString();

                            var projectRef = (await _projectRepository.GetProjectByName(row?.GetCell(0)?.ToString()))?.Id;

                            if (projectRef == null)
                                projectRef = await _projectRepository.InsertAndSaveAsync(new Project
                                {
                                    Title = row?.GetCell(0)?.ToString(),
                                    Code = Guid.NewGuid().ToString(),
                                    ProjectStatus = ProjectStatus.NotStarted,
                                    IsDeleted = false
                                });

                            var birth = row?.GetCell(7)?.ToString().Split("/");

                            var bankaccount = await _bankAccountRepository.InsertAndSaveAsync(new BankAccount
                            {
                                AccountNumber = row?.GetCell(14)?.ToString(),
                                Title = row?.GetCell(15)?.ToString(),
                                IsDeleted = false,
                                CreatedByRef = _userManager.Users.Where(m => m.UserName == "user").FirstOrDefault().Id,
                                CreatedDate = DateTime.Now,
                            });

                            //if (personnelCode == "120110")
                            //    personnelCode = "120110";

                            var user = new ApplicationUser
                            {
                                ProjectRef = projectRef,
                                PersonnelCode = row?.GetCell(1)?.ToString(),
                                UserName = row?.GetCell(1)?.ToString(),
                                NationalCode = row?.GetCell(2)?.ToString(),
                                InsuranceCode = row?.GetCell(3)?.ToString(),
                                FirstName = row?.GetCell(4)?.ToString(),
                                LastName = row?.GetCell(5)?.ToString(),
                                FatherName = row?.GetCell(6)?.ToString(),
                                Birthday = row?.GetCell(7)?.ToString() == null || row?.GetCell(7)?.ToString() == "" ? null : new DateTime(Convert.ToInt32(birth[0]), Convert.ToInt32(birth[1]), Convert.ToInt32(birth[2]), new PersianCalendar()),
                                BirthPlace = row?.GetCell(8)?.ToString(),
                                IdentityNumber = row?.GetCell(9)?.ToString(),
                                IdentitySerialNumber = row?.GetCell(10)?.ToString(),
                                Nationality = row?.GetCell(11)?.ToString(),
                                DegreeOfEducation = row?.GetCell(12)?.ToString(),
                                JobTitle = row?.GetCell(13)?.ToString(),
                                BankAccountRef = bankaccount,
                                NumberOfChildren = Convert.ToByte(row?.GetCell(16)?.ToString()),
                                DailySalary = Convert.ToInt32(Math.Round(Convert.ToDouble(row?.GetCell(17)?.ToString()))),
                                DailyBaseYear = Convert.ToInt32(Math.Round(Convert.ToDouble(row?.GetCell(18)?.ToString()))),
                                FoodAndHouseRight = Convert.ToInt32(Math.Round(Convert.ToDouble(row?.GetCell(19)?.ToString()))),
                                WorkerRight = Convert.ToInt32(Math.Round(Convert.ToDouble(row?.GetCell(20)?.ToString()))),
                                ChildrenRight = Convert.ToInt32(Math.Round(Convert.ToDouble(row?.GetCell(21)?.ToString()))),
                                MonthlySalary = Convert.ToInt32(Math.Round(Convert.ToDouble(row?.GetCell(22)?.ToString()))),
                                MonthlyBaseYear = Convert.ToInt32(Math.Round(Convert.ToDouble(row?.GetCell(23)?.ToString()))),
                                Address = row?.GetCell(24)?.ToString(),
                                ZipCode = row?.GetCell(25)?.ToString(),
                                PhoneNumber = row?.GetCell(26)?.ToString(),
                                InsuranceHistory = Convert.ToInt32(Math.Round(Convert.ToDouble(row?.GetCell(27)?.ToString()))),
                                WorkExperience = Convert.ToInt32(Math.Round(Convert.ToDouble(row?.GetCell(28)?.ToString()))),
                                MaritalStatus = EnumHelper<MaritalStatus>.Parse(row?.GetCell(29)?.ToString()),
                                MilitaryService = EnumHelper<MilitaryService>.Parse(row?.GetCell(30)?.ToString()),
                                JobCode = row?.GetCell(31)?.ToString(),
                                Gender = EnumHelper<Gender>.Parse(row?.GetCell(32)?.ToString()),
                                HireDate = row?.GetCell(33)?.ToString() == null || row?.GetCell(33)?.ToString() == "" ? null : Convert.ToDateTime(row?.GetCell(33)?.ToString()),
                                StartWorkingDate = row?.GetCell(34)?.ToString() == null || row?.GetCell(34)?.ToString() == "" ? null : Convert.ToDateTime(row?.GetCell(34)?.ToString()),
                                EndWorkingDate = row?.GetCell(35)?.ToString() == null || row?.GetCell(35)?.ToString() == "" ? null : Convert.ToDateTime(row?.GetCell(35)?.ToString()),
                                IsDeleted = false,
                                IsActive = true,
                                IsProfileCompleted = true,
                                IsBlocked = false,
                                EmailConfirmed = true,
                                PhoneNumberConfirmed = true,
                                TwoFactorEnabled = false
                            };

                            var result = await _userManager.CreateAsync(user, row?.GetCell(2)?.ToString());

                            if (result.Succeeded)
                            {
                                user = _userManager.Users.Where(m => m.UserName == personnelCode).FirstOrDefault();

                                await _userManager.AddToRoleAsync(user, Roles.User.ToString());

                                count++;
                            }
                            else
                            {
                                _notify.Error("اضافه کردن فایل با خطا مواجعه شد.");

                                if (personnelCode != "")
                                    _notify.Error($@"سیستم در وارد کردن {personnelCode} به خطا خورد.");

                                _notify.Error(@$"متن خطا سمت سیستم {result.Errors}");

                                _notify.Success($@"{count} داده به سیستم اضافه شد.");

                                break;
                            }
                        }

                        _notify.Success($@"{count} داده به سیستم اضافه شد.");

                        _notify.Success("کاربران با موفقیت به سیستم اضافه شدند.");
                    }
                }
                return true;
            }
            catch (System.Exception e)
            {
                _notify.Success($@"{count} داده به سیستم اضافه شد.");

                _notify.Error("اضافه کردن فایل با خطا مواجعه شد.");

                if (personnelCode != "")
                    _notify.Error($@"سیستم در وارد کردن {personnelCode} به خطا خورد.");

                return false;
            }
        }

        [HttpPost]
        public async Task<bool> ImportAttendances()
        {
            try
            {
                IFormFile file = Request.Form.Files[0];

                string folderName = "UploadExcel";

                string webRootPath = _hostingEnvironment.WebRootPath;

                string newPath = Path.Combine(webRootPath, folderName);

                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }

                if (file.Length > 0)
                {
                    string sFileExtension = Path.GetExtension(file.FileName).ToLower();

                    ISheet sheet;

                    string fullPath = Path.Combine(newPath, file.FileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);

                        stream.Position = 0;

                        if (sFileExtension == ".xls")
                        {
                            HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  

                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                        }

                        else
                        {
                            XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  

                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                        }

                        for (int j = 1; j < sheet.LastRowNum + 1; j++)
                        {
                            var row = sheet.GetRow(j);

                            var model = new Imported
                            {
                                PersonnelCode = row?.GetCell(0)?.ToString(),
                                Name = row?.GetCell(1)?.ToString(),
                                FamilyName = row?.GetCell(2)?.ToString(),
                                JobTitle = row?.GetCell(3)?.ToString(),
                                Province = row?.GetCell(4)?.ToString(),
                                AccountNumber = row?.GetCell(5)?.ToString(),
                                NationalCode = row?.GetCell(6)?.ToString(),
                                InsuranceNumber = row?.GetCell(7)?.ToString(),
                                ServiceLocation = row?.GetCell(8)?.ToString(),
                                DegreeEducation = row?.GetCell(9)?.ToString(),
                                DurationOperation = row?.GetCell(10)?.ToString(),
                                OvertimeworkingTime = row?.GetCell(11)?.ToString(),
                                NightworkingTime = row?.GetCell(12)?.ToString(),
                                HolidayworkingTime = row?.GetCell(13)?.ToString(),
                                MissionTime = row?.GetCell(14)?.ToString(),
                                FoodTime = row?.GetCell(15)?.ToString(),
                                NumberChildren = row?.GetCell(16)?.ToString(),
                                Salary = row?.GetCell(17)?.ToString(),
                                SeveranceDaily = row?.GetCell(18)?.ToString(),
                                DailyPay = row?.GetCell(19)?.ToString(),
                                FoodAndHousingRight = row?.GetCell(20)?.ToString(),
                                WorkerRight = row?.GetCell(21)?.ToString(),
                                OvertimePay = row?.GetCell(22)?.ToString(),
                                MonthlyPay = row?.GetCell(23)?.ToString(),
                                OvertimeworkingPay = row?.GetCell(24)?.ToString(),
                                ChildrenRightPay = row?.GetCell(25)?.ToString(),
                                HouseRightPay = row?.GetCell(26)?.ToString(),
                                WorkerRightPay = row?.GetCell(27)?.ToString(),
                                NightworkingPay = row?.GetCell(28)?.ToString(),
                                HolidayworkingPay = row?.GetCell(29)?.ToString(),
                                MissionPay = row?.GetCell(30)?.ToString(),
                                FoodPay = row?.GetCell(31)?.ToString(),
                                Other01 = row?.GetCell(32)?.ToString(),
                                Other02 = row?.GetCell(33)?.ToString(),
                                Disparity = row?.GetCell(34)?.ToString(),
                                PreviousReceipt = row?.GetCell(35)?.ToString(),
                                SumSalaryAndBenefit = row?.GetCell(36)?.ToString(),
                                TaxationPay = row?.GetCell(37)?.ToString(),
                                InsurancePay = row?.GetCell(38)?.ToString(),
                                Insurance7Percent = row?.GetCell(39)?.ToString(),
                                Taxation = row?.GetCell(40)?.ToString(),
                                HelpPay = row?.GetCell(41)?.ToString(),
                                Absence = row?.GetCell(42)?.ToString(),
                                Debt = row?.GetCell(43)?.ToString(),
                                OtherDeductions = row?.GetCell(44)?.ToString(),
                                SumDeductions = row?.GetCell(45)?.ToString(),
                                PureIncome = row?.GetCell(46)?.ToString(),
                                Year = row?.GetCell(47)?.ToString(),
                                Month = row?.GetCell(48)?.ToString(),
                                SeveranceMonthly = row?.GetCell(49)?.ToString(),
                                ShiftWorkTime = row?.GetCell(50)?.ToString(),
                                ShiftWorkPay = row?.GetCell(51)?.ToString(),
                                SupplementaryInsuranceDeduction = row?.GetCell(52)?.ToString(),
                                RewardTime = row?.GetCell(53)?.ToString(),
                                RewardPay = row?.GetCell(54)?.ToString(),
                                YearsPay = row?.GetCell(55)?.ToString(),
                                FestalPay = row?.GetCell(56)?.ToString(),
                                BasicOverTimePay = row?.GetCell(57)?.ToString(),
                                SupplementaryInsuranceSupervisor = row?.GetCell(58)?.ToString(),
                                SupplementaryInsuranceForDependents = row?.GetCell(59)?.ToString(),
                                NonDependentSupplementaryInsurance = row?.GetCell(60)?.ToString(),
                                WelfareCostPay = row?.GetCell(61)?.ToString(),
                                TransportationPay = row?.GetCell(62)?.ToString(),
                                DelayedTime = row?.GetCell(63)?.ToString(),
                                InstitutionaLoan = row?.GetCell(64)?.ToString(),
                                SamanLoan = row?.GetCell(65)?.ToString(),
                                DelayedTransportationPay = row?.GetCell(66)?.ToString(),
                                DelayedSupplementaryInsuranceDeduction = row?.GetCell(67)?.ToString(),
                                WelfareAllowancePay = row?.GetCell(68)?.ToString(),
                                PerformancePay = row?.GetCell(69)?.ToString(),
                                MonthlyBenefits = row?.GetCell(70)?.ToString(),
                                MonthlyWagesAndBenefitsIncluded = row?.GetCell(71)?.ToString(),
                                IncludedAndNotIncluded = row?.GetCell(72)?.ToString(),
                                UnemploymentInsurance = row?.GetCell(73)?.ToString(),
                                Insurance30Percent = row?.GetCell(74)?.ToString(),
                                EmployerShareInsurance = row?.GetCell(75)?.ToString(),
                                ContinuousBasicRightsToHousingAndChildrenRights = row?.GetCell(76)?.ToString(),
                                ContinuousBaseSalaryAndBaseYears = row?.GetCell(77)?.ToString(),
                                NonContinuousIncludedNotIncluded = row?.GetCell(78)?.ToString(),
                                NonContinuousIncluded = row?.GetCell(79)?.ToString(),
                            };

                            await _repository.InsertAsync(model);
                        }
                        if (_repository.SaveChanges() > 0)
                            _notify.Success("فایل با موفقیت در سیستم اضافه شد.");
                        else
                            _notify.Error("اضافه کردن فایل با خطا مواجعه شد.");
                    }
                }
                return true;
            }
            catch (System.Exception e)
            {
                _notify.Error("اضافه کردن فایل با خطا مواجعه شد.");

                return false;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAttendances()
        {
            var listOfAttendances = await _repository.GetListAsync();

            foreach (var item in listOfAttendances)
            {
                await _repository.DeleteAsync(item);
            }

            await _repository.SaveChangesAsync();

            _notify.Success("کارکرد با موفقیت حذف شد .");

            return Redirect("~/dashboard/managment");
        }

        [HttpGet]
        public async Task<IActionResult> DeletePersonnel()
        {
            var listOfAttendances = await _userManager.Users.ToListAsync();

            foreach (var item in listOfAttendances)
            {
                var role = await _userManager.GetRolesAsync(item);

                if (role.FirstOrDefault() == "User")
                    await _userManager.DeleteAsync(item);
            }

            var bankaccs = await _bankAccountRepository.GetListAsync();

            foreach (var item in bankaccs)
            {
                await _bankAccountRepository.DeleteAsync(item);
            }

            await _bankAccountRepository.SaveChangesAsync();

            _notify.Success("پرسنل با موفقیت حذف شد .");

            return Redirect("~/");
        }
    }
}