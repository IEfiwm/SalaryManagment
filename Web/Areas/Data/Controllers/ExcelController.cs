using Application.Enums;
using Application.Extensions;
using Application.Extensions.DataConversion;
using Common.Enums;
using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Domain.Entities.Data;
using Infrastructure.Repositories.Application;
using Infrastructure.Repositories.Application.Basic;
using Infrastructure.Repositories.Application.Idenitity;
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
        private readonly IUserRepository _userRepository;

        public ExcelController(IHostingEnvironment hostingEnvironment,
            IimportedRepository repository,
            UserManager<ApplicationUser> userManager,
            IBankAccountRepository bankAccountRepository,
            IProjectRepository projectRepository,
            IUserRepository userRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _repository = repository;
            _userManager = userManager;
            _bankAccountRepository = bankAccountRepository;
            _projectRepository = projectRepository;
            _userRepository = userRepository;
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

                            var model = new Imported();

                            #region Validaition
                            if (string.IsNullOrEmpty(row?.GetCell(2)?.ToString()))
                                continue;
                            if (!DataConversion.Convert<decimal>(row?.GetCell(2)?.ToString(), out decimal nationalCode))
                            {
                                _notify.Error("قالب داده صحیح نیست : کد ملی ردیف: " + j);
                                return false;
                            }
                            //string Name;
                            //if (!DataConversion.Convert<string>(row?.GetCell(1)?.ToString(), out Name))
                            //{
                            //    _notify.Error("قالب داده صحیح نیست : نام ردیف: " + j);
                            //    return false;
                            //}
                            model.Name = row?.GetCell(0)?.ToString();
                            //string FamilyName;
                            //if (!DataConversion.Convert<string>(row?.GetCell(2)?.ToString(), out FamilyName))
                            //{
                            //    _notify.Error("قالب داده صحیح نیست : نام خانوادگی ردیف: " + j);
                            //    return false;
                            //}
                            model.FamilyName = row?.GetCell(1)?.ToString();
                            //string JobTitle;
                            //if (!DataConversion.Convert<string>(row?.GetCell(3)?.ToString(), out JobTitle))
                            //{
                            //    _notify.Error("قالب داده صحیح نیست : شغل ردیف: " + j);
                            //    return false;
                            //}
                            model.NationalCode = row?.GetCell(2)?.ToString();
                            //string InsuranceNumber;
                            //if (!DataConversion.Convert<string>(row?.GetCell(7)?.ToString(), out InsuranceNumber))
                            //{
                            //    _notify.Error("قالب داده صحیح نیست : شماره بیمه ردیف: " + j);
                            //    return false;
                            //}
                            if (!string.IsNullOrEmpty(row?.GetCell(3)?.ToString()))
                            {
                                if (!DataConversion.Convert<int>(row?.GetCell(3)?.ToString(), out int durationOperation))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارکرد موثر ردیف: " + j);
                                    return false;
                                }
                                model.DurationOperation = row?.GetCell(3)?.ToString();
                            }
                            else
                            {
                                model.DurationOperation = "0";
                            }
                            if (!string.IsNullOrEmpty(row?.GetCell(4)?.ToString()))
                            {

                                if (!DataConversion.Convert<int>(row?.GetCell(4)?.ToString(), out int overtimeworkingTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارکرد اضافه کاری ردیف: " + j);
                                    return false;
                                }
                                model.OvertimeworkingTime = row?.GetCell(4)?.ToString();
                            }
                            else
                            {
                                model.OvertimeworkingTime = "0";
                            }
                            if (!string.IsNullOrEmpty(row?.GetCell(5)?.ToString()))
                            {

                                if (!DataConversion.Convert<int>(row?.GetCell(5)?.ToString(), out int nightworkingTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارکرد شبکاری ردیف: " + j);
                                    return false;
                                }
                                model.NightworkingTime = row?.GetCell(5)?.ToString();
                            }
                            else
                            {
                                model.NightworkingTime = "0";
                            }
                            if (!string.IsNullOrEmpty(row?.GetCell(6)?.ToString()))
                            {

                                if (!DataConversion.Convert<int>(row?.GetCell(6)?.ToString(), out int holidayworkingTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارکرد تعطیل کاری ردیف: " + j);
                                    return false;
                                }
                                model.HolidayworkingTime = row?.GetCell(6)?.ToString();
                            }
                            else
                            {
                                model.HolidayworkingTime = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(7)?.ToString()))
                            {
                                if (!DataConversion.Convert<int>(row?.GetCell(7)?.ToString(), out int missionTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارکرد ماموریت ردیف: " + j);
                                    return false;
                                }
                                model.MissionTime = row?.GetCell(7)?.ToString();
                            }
                            else
                            {
                                model.MissionTime = "0";
                            }
                            if (!string.IsNullOrEmpty(row?.GetCell(8)?.ToString()))
                            {

                                if (!DataConversion.Convert<int>(row?.GetCell(8)?.ToString(), out int foodTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : كاركرد حق غذا ردیف: " + j);
                                    return false;
                                }
                                model.FoodTime = row?.GetCell(8)?.ToString();
                            }
                            else
                            {
                                model.FoodTime = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(9)?.ToString()))
                            {
                                if (!DataConversion.Convert<int>(row?.GetCell(9)?.ToString(), out int numberChildren))
                                {
                                    _notify.Error("قالب داده صحیح نیست : تعداد فرزندان ردیف: " + j);
                                    return false;
                                }
                                model.NumberChildren = row?.GetCell(9)?.ToString();
                            }
                            else
                            {
                                model.NumberChildren = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(10)?.ToString()))
                            {

                                if (!DataConversion.Convert<decimal>(row?.GetCell(10)?.ToString(), out decimal salary))
                                {
                                    _notify.Error("قالب داده صحیح نیست : دستمزد ردیف: " + j);
                                    return false;
                                }
                                model.Salary = row?.GetCell(10)?.ToString();
                            }
                            else
                            {
                                model.Salary = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(11)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(11)?.ToString(), out decimal severanceDaily))
                                {
                                    _notify.Error("قالب داده صحیح نیست : پایه سنوات ردیف: " + j);
                                    return false;
                                }
                                model.SeveranceDaily = row?.GetCell(11)?.ToString();
                            }
                            else
                            {
                                model.SeveranceDaily = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(12)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(12)?.ToString(), out decimal dailyPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مزد روزانه ردیف: " + j);
                                    return false;
                                }
                                model.DailyPay = row?.GetCell(12)?.ToString();
                            }
                            else
                            {
                                model.DailyPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(13)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(13)?.ToString(), out decimal foodAndHousingRight))
                                {
                                    _notify.Error("قالب داده صحیح نیست : حق خواربار و مسکن ردیف: " + j);
                                    return false;
                                }
                                model.FoodAndHousingRight = row?.GetCell(13)?.ToString();
                            }
                            else
                            {
                                model.FoodAndHousingRight = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(14)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(14)?.ToString(), out decimal workerRight))
                                {
                                    _notify.Error("قالب داده صحیح نیست : بن کارگر ردیف: " + j);
                                    return false;
                                }
                                model.WorkerRight = row?.GetCell(14)?.ToString();
                            }
                            else
                            {
                                model.WorkerRight = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(15)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(15)?.ToString(), out decimal overtimePay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : اضافه کاری ثابت ردیف: " + j);
                                    return false;
                                }
                                model.OvertimePay = row?.GetCell(15)?.ToString();
                            }
                            else
                            {
                                model.OvertimePay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(16)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(16)?.ToString(), out decimal monthlyPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : سنوات ردیف: " + j);
                                    return false;
                                }
                                model.MonthlyPay = row?.GetCell(16)?.ToString();
                            }
                            else
                            {
                                model.MonthlyPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(17)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(17)?.ToString(), out decimal overtimeworkingPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : اضافه کاری ردیف: " + j);
                                    return false;
                                }
                                model.OvertimeworkingPay = row?.GetCell(17)?.ToString();
                            }
                            else
                            {
                                model.OvertimeworkingPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(18)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(18)?.ToString(), out decimal childrenRightPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : حق اولاد ردیف: " + j);
                                    return false;
                                }
                                model.ChildrenRightPay = row?.GetCell(18)?.ToString();
                            }
                            else
                            {
                                model.ChildrenRightPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(19)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(19)?.ToString(), out decimal houseRightPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : حق مسکن ردیف: " + j);
                                    return false;
                                }
                                model.HouseRightPay = row?.GetCell(19)?.ToString();
                            }
                            else
                            {
                                model.HouseRightPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(20)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(20)?.ToString(), out decimal workerRightPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : بن کارگری ردیف: " + j);
                                    return false;
                                }
                                model.WorkerRightPay = row?.GetCell(20)?.ToString();
                            }
                            else
                            {
                                model.WorkerRightPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(21)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(21)?.ToString(), out decimal nightworkingPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : شب کاری ردیف: " + j);
                                    return false;
                                }
                                model.NightworkingPay = row?.GetCell(21)?.ToString();
                            }
                            else
                            {
                                model.NightworkingPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(22)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(22)?.ToString(), out decimal holidayworkingPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : تعطیل کاری ردیف: " + j);
                                    return false;
                                }
                                model.HolidayworkingPay = row?.GetCell(22)?.ToString();
                            }
                            else
                            {
                                model.HolidayworkingPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(23)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(23)?.ToString(), out decimal missionPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : ماموریت ردیف: " + j);
                                    return false;
                                }
                                model.MissionPay = row?.GetCell(23)?.ToString();
                            }
                            else
                            {
                                model.MissionPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(24)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(24)?.ToString(), out decimal foodPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : هزینه غذا ردیف: " + j);
                                    return false;
                                }
                                model.FoodPay = row?.GetCell(24)?.ToString();
                            }
                            else
                            {
                                model.FoodPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(25)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(25)?.ToString(), out decimal other01))
                                {
                                    _notify.Error("قالب داده صحیح نیست : سایر1 ردیف: " + j);
                                    return false;
                                }
                                model.Other01 = row?.GetCell(25)?.ToString();
                            }
                            else
                            {
                                model.Other01 = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(26)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(26)?.ToString(), out decimal other02))
                                {
                                    _notify.Error("قالب داده صحیح نیست :سایر2 ردیف: " + j);
                                    return false;
                                }
                                model.Other02 = row?.GetCell(26)?.ToString();
                            }
                            else
                            {
                                model.Other02 = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(27)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(27)?.ToString(), out decimal disparity))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مابتفاوت ردیف: " + j);
                                    return false;
                                }
                                model.Disparity = row?.GetCell(27)?.ToString();
                            }
                            else
                            {
                                model.Disparity = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(28)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(28)?.ToString(), out decimal previousReceipt))
                                {
                                    _notify.Error("قالب داده صحیح نیست : طلب از قبل ردیف: " + j);
                                    return false;
                                }
                                model.PreviousReceipt = row?.GetCell(28)?.ToString();
                            }
                            else
                            {
                                model.PreviousReceipt = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(29)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(29)?.ToString(), out decimal sumSalaryAndBenefit))
                                {
                                    _notify.Error("قالب داده صحیح نیست : جمع حقوق و مزایا ردیف: " + j);
                                    return false;
                                }
                                model.SumSalaryAndBenefit = row?.GetCell(29)?.ToString();
                            }
                            else
                            {
                                model.SumSalaryAndBenefit = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(30)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(30)?.ToString(), out decimal taxationPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مشمول مالیات ردیف: " + j);
                                    return false;
                                }
                                model.TaxationPay = row?.GetCell(30)?.ToString();
                            }
                            else
                            {
                                model.TaxationPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(31)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(31)?.ToString(), out decimal insurancePay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مشمول بیمه ردیف: " + j);
                                    return false;
                                }
                                model.InsurancePay = row?.GetCell(31)?.ToString();
                            }
                            else
                            {
                                model.InsurancePay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(32)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(32)?.ToString(), out decimal insurance7Percent))
                                {
                                    _notify.Error("قالب داده صحیح نیست : بیمه 7% ردیف: " + j);
                                    return false;
                                }
                                model.Insurance7Percent = row?.GetCell(32)?.ToString();
                            }
                            else
                            {
                                model.Insurance7Percent = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(33)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(33)?.ToString(), out decimal taxation))
                                {
                                    _notify.Error("قالب داده صحیح نیست : ماليات ردیف: " + j);
                                    return false;
                                }
                                model.Taxation = row?.GetCell(33)?.ToString();
                            }
                            else
                            {
                                model.Taxation = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(34)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(34)?.ToString(), out decimal helpPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مساعده ردیف: " + j);
                                    return false;
                                }
                                model.HelpPay = row?.GetCell(34)?.ToString();
                            }
                            else
                            {
                                model.HelpPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(35)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(35)?.ToString(), out decimal absence))
                                {
                                    _notify.Error("قالب داده صحیح نیست : غیبت ردیف: " + j);
                                    return false;
                                }
                                model.Absence = row?.GetCell(35)?.ToString();
                            }
                            else
                            {
                                model.Absence = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(36)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(36)?.ToString(), out decimal debt))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مبلغ بدهی از قبل ردیف: " + j);
                                    return false;
                                }
                                model.Debt = row?.GetCell(36)?.ToString();
                            }
                            else
                            {
                                model.Debt = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(37)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(37)?.ToString(), out decimal otherDeductions))
                                {
                                    _notify.Error("قالب داده صحیح نیست : سایر کسورات ردیف: " + j);
                                    return false;
                                }
                                model.OtherDeductions = row?.GetCell(37)?.ToString();
                            }
                            else
                            {
                                model.OtherDeductions = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(38)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(38)?.ToString(), out decimal sumDeductions))
                                {
                                    _notify.Error("قالب داده صحیح نیست : جمع كسورات ردیف: " + j);
                                    return false;
                                }
                                model.SumDeductions = row?.GetCell(38)?.ToString();
                            }
                            else
                            {
                                model.SumDeductions = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(39)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(39)?.ToString(), out decimal pureIncome))
                                {
                                    _notify.Error("قالب داده صحیح نیست : خالص دریافتی ردیف: " + j);
                                    return false;
                                }
                                model.PureIncome = row?.GetCell(39)?.ToString();
                            }
                            else
                            {
                                model.PureIncome = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(40)?.ToString()))
                            {
                                if (!DataConversion.Convert<int>(row?.GetCell(40)?.ToString(), out int year))
                                {
                                    _notify.Error("قالب داده صحیح نیست : سال ردیف: " + j);
                                    return false;
                                }
                                model.Year = row?.GetCell(40)?.ToString();
                            }
                            else
                            {
                                model.Year = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(41)?.ToString()))
                            {
                                if (!DataConversion.Convert<int>(row?.GetCell(41)?.ToString(), out int month))
                                {
                                    _notify.Error("قالب داده صحیح نیست : ماه ردیف: " + j);
                                    return false;
                                }
                                model.Month = row?.GetCell(41)?.ToString();
                            }
                            else
                            {
                                model.Month = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(42)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(42)?.ToString(), out decimal severanceMonthly))
                                {
                                    _notify.Error("قالب داده صحیح نیست : پایه سنوات مزایا ردیف: " + j);
                                    return false;
                                }
                                model.SeveranceMonthly = row?.GetCell(42)?.ToString();
                            }
                            else
                            {
                                model.SeveranceMonthly = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(43)?.ToString()))
                            {
                                if (!DataConversion.Convert<int>(row?.GetCell(43)?.ToString(), out int shiftWorkTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست :  نوبت کاری-کارکرد ردیف: " + j);
                                    return false;
                                }
                                model.ShiftWorkTime = row?.GetCell(43)?.ToString();
                            }
                            else
                            {
                                model.ShiftWorkTime = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(44)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(44)?.ToString(), out decimal shiftWorkPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : نوبتکاری -مزایا ردیف: " + j);
                                    return false;
                                }
                                model.ShiftWorkPay = row?.GetCell(44)?.ToString();
                            }
                            else
                            {
                                model.ShiftWorkPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(45)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(45)?.ToString(), out decimal supplementaryInsuranceDeduction))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کسر بیمه تکمیلی - کسورات ردیف: " + j);
                                    return false;
                                }
                                model.SupplementaryInsuranceDeduction = row?.GetCell(45)?.ToString();
                            }
                            else
                            {
                                model.SupplementaryInsuranceDeduction = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(46)?.ToString()))
                            {
                                if (!DataConversion.Convert<int>(row?.GetCell(46)?.ToString(), out int rewardTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : پاداش - کارکرد ردیف: " + j);
                                    return false;
                                }
                                model.RewardTime = row?.GetCell(46)?.ToString();
                            }
                            else
                            {
                                model.RewardTime = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(47)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(47)?.ToString(), out decimal yearsPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : سنوات-مزایا ردیف: " + j);
                                    return false;
                                }
                                model.YearsPay = row?.GetCell(47)?.ToString();
                            }
                            else
                            {
                                model.YearsPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(48)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(48)?.ToString(), out decimal rewardPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : عیدی - مزایا  ردیف: " + j);
                                    return false;
                                }
                                model.FestalPay = row?.GetCell(48)?.ToString();
                            }
                            else
                            {
                                model.FestalPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(49)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(49)?.ToString(), out decimal festalPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : پاداش - مزایا ردیف: " + j);
                                    return false;
                                }
                                model.RewardPay = row?.GetCell(49)?.ToString();
                            }
                            else
                            {
                                model.RewardPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(50)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(50)?.ToString(), out decimal basicOverTimePay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مبلغ اضافه کاری ثابت-مزایا ردیف: " + j);
                                    return false;
                                }
                                model.BasicOverTimePay = row?.GetCell(50)?.ToString();
                            }
                            else
                            {
                                model.BasicOverTimePay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(51)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(58)?.ToString(), out decimal supplementaryInsuranceSupervisor))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کسر بیمه تکمیلی سرپرست ردیف: " + j);
                                    return false;
                                }
                                model.SupplementaryInsuranceSupervisor = row?.GetCell(58)?.ToString();
                            }
                            else
                            {
                                model.SupplementaryInsuranceSupervisor = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(52)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(52)?.ToString(), out decimal supplementaryInsuranceForDependents))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کسر بیمه تکمیلی افراد تحت تکفل ردیف: " + j);
                                    return false;
                                }
                                model.SupplementaryInsuranceForDependents = row?.GetCell(52)?.ToString();
                            }
                            else
                            {
                                model.SupplementaryInsuranceForDependents = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(53)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(53)?.ToString(), out decimal nonDependentSupplementaryInsurance))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کسر بیمه تکمیلی افرادغیر تحت تکفل ردیف: " + j);
                                    return false;
                                }
                                model.NonDependentSupplementaryInsurance = row?.GetCell(53)?.ToString();
                            }
                            else
                            {
                                model.NonDependentSupplementaryInsurance = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(54)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(54)?.ToString(), out decimal welfareCostPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : هزینه رفاهی ردیف: " + j);
                                    return false;
                                }
                                model.WelfareCostPay = row?.GetCell(54)?.ToString();
                            }
                            else
                            {
                                model.WelfareCostPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(55)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(55)?.ToString(), out decimal transportationPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : ایاب و ذهاب ردیف: " + j);
                                    return false;
                                }
                                model.TransportationPay = row?.GetCell(55)?.ToString();
                            }
                            else
                            {
                                model.TransportationPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(56)?.ToString()))
                            {
                                if (!DataConversion.Convert<int>(row?.GetCell(56)?.ToString(), out int delayedTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارکرد معوقه ردیف: " + j);
                                    return false;
                                }
                                model.DelayedTime = row?.GetCell(56)?.ToString();
                            }
                            else
                            {
                                model.DelayedTime = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(57)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(57)?.ToString(), out decimal institutionaLoan))
                                {
                                    _notify.Error("قالب داده صحیح نیست : وام موسسه ردیف: " + j);
                                    return false;
                                }
                                model.InstitutionaLoan = row?.GetCell(57)?.ToString();
                            }
                            else
                            {
                                model.InstitutionaLoan = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(58)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(58)?.ToString(), out decimal samanLoan))
                                {
                                    _notify.Error("قالب داده صحیح نیست : وام سامان ردیف: " + j);
                                    return false;
                                }
                                model.SamanLoan = row?.GetCell(58)?.ToString();
                            }
                            else
                            {
                                model.SamanLoan = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(59)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(59)?.ToString(), out decimal delayedTransportationPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : معوقه ایاب و ذهاب ردیف: " + j);
                                    return false;
                                }
                                model.DelayedTransportationPay = row?.GetCell(59)?.ToString();
                            }
                            else
                            {
                                model.DelayedTransportationPay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(60)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(60)?.ToString(), out decimal delayedSupplementaryInsuranceDeduction))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کسر بیمه تکمیلی ماه معوقه ردیف: " + j);
                                    return false;
                                }
                                model.DelayedSupplementaryInsuranceDeduction = row?.GetCell(60)?.ToString();
                            }
                            else
                            {
                                model.DelayedSupplementaryInsuranceDeduction = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(61)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(61)?.ToString(), out decimal welfareAllowancePay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کمک هزینه رفاهی ردیف: " + j);
                                    return false;
                                }
                                model.WelfareAllowancePay = row?.GetCell(61)?.ToString();
                            }
                            else
                            {
                                model.WelfareAllowancePay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(62)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(62)?.ToString(), out decimal performancePay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارایی ردیف: " + j);
                                    return false;
                                }
                                model.PerformancePay = row?.GetCell(62)?.ToString();
                            }
                            else
                            {
                                model.PerformancePay = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(63)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(63)?.ToString(), out decimal daily))
                                {
                                    _notify.Error("قالب داده صحیح نیست : روزانه ردیف: " + j);
                                    return false;
                                }
                                model.Daily = row?.GetCell(63)?.ToString();
                            }
                            else
                            {
                                model.Daily = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(64)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(64)?.ToString(), out decimal monthly))
                                {
                                    _notify.Error("قالب داده صحیح نیست : ماهانه ردیف: " + j);
                                    return false;
                                }
                                model.Monthly = row?.GetCell(64)?.ToString();
                            }
                            else
                            {
                                model.Monthly = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(65)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(65)?.ToString(), out decimal monthlyBenefits))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مزایای ماهانه ردیف: " + j);
                                    return false;
                                }
                                model.MonthlyBenefits = row?.GetCell(65)?.ToString();
                            }
                            else
                            {
                                model.MonthlyBenefits = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(66)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(66)?.ToString(), out decimal monthlyWagesAndBenefitsIncluded))
                                {
                                    _notify.Error("قالب داده صحیح نیست : دستمزد و مزایای مشمول ماهانه ردیف: " + j);
                                    return false;
                                }
                                model.MonthlyWagesAndBenefitsIncluded = row?.GetCell(66)?.ToString();
                            }
                            else
                            {
                                model.MonthlyWagesAndBenefitsIncluded = "0";
                            }
            
                            if (!string.IsNullOrEmpty(row?.GetCell(67)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(67)?.ToString(), out decimal includedAndNotIncluded))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مشمول و غیر مشمول ردیف: " + j);
                                    return false;
                                }
                                model.IncludedAndNotIncluded = row?.GetCell(67)?.ToString();
                            }
                            else
                            {
                                model.IncludedAndNotIncluded = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(68)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(68)?.ToString(), out decimal unemploymentInsurance))
                                {
                                    _notify.Error("قالب داده صحیح نیست : بیمه بیکاری ردیف: " + j);
                                    return false;
                                }
                                model.UnemploymentInsurance = row?.GetCell(68)?.ToString();
                            }
                            else
                            {
                                model.UnemploymentInsurance = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(69)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(69)?.ToString(), out decimal insurance30Percent))
                                {
                                    _notify.Error("قالب داده صحیح نیست : بیمه 30% ردیف: " + j);
                                    return false;
                                }
                                model.Insurance30Percent = row?.GetCell(69)?.ToString();
                            }
                            else
                            {
                                model.Insurance30Percent = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(70)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(70)?.ToString(), out decimal employerShareInsurance))
                                {
                                    _notify.Error("قالب داده صحیح نیست : بیمه سهم کارفرما ردیف: " + j);
                                    return false;
                                }
                                model.EmployerShareInsurance = row?.GetCell(70)?.ToString();
                            }
                            else
                            {
                                model.EmployerShareInsurance = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(71)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(71)?.ToString(), out decimal continuousBasicRightsToHousingAndChildrenRights))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مستمر - حقوق پایه بن و مسکن و حق اولاد ردیف: " + j);
                                    return false;
                                }
                                model.ContinuousBasicRightsToHousingAndChildrenRights = row?.GetCell(71)?.ToString();
                            }
                            else
                            {
                                model.ContinuousBasicRightsToHousingAndChildrenRights = "0";
                            }

                            if (!string.IsNullOrEmpty(row?.GetCell(72)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(72)?.ToString(), out decimal continuousBaseSalaryAndBaseYears))
                                {
                                    _notify.Error("قالب داده صحیح نیست : معوقه حقوق پایه و پایه سنوات ردیف: " + j);
                                    return false;
                                }
                                model.ContinuousBaseSalaryAndBaseYears = row?.GetCell(72)?.ToString();
                            }
                            else
                            {
                                model.ContinuousBaseSalaryAndBaseYears = "0";
                            }
                            
                            if (!string.IsNullOrEmpty(row?.GetCell(73)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(73)?.ToString(), out decimal nonContinuousIncludedNotIncluded))
                                {
                                    _notify.Error("قالب داده صحیح نیست : غیر مستمر - مشمول و غیر مشمول ردیف: " + j);
                                    return false;
                                }
                                model.NonContinuousIncludedNotIncluded = row?.GetCell(73)?.ToString();
                            }
                            else
                            {
                                model.NonContinuousIncludedNotIncluded = "0";
                            }
                            
                            if (!string.IsNullOrEmpty(row?.GetCell(74)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(74)?.ToString(), out decimal nonContinuousIncluded))
                                {
                                    _notify.Error("قالب داده صحیح نیست : غیر مستمر - مشمول ردیف: " + j);
                                    return false;
                                }
                                model.NonContinuousIncluded = row?.GetCell(74)?.ToString();
                            }
                            else
                            {
                                model.NonContinuousIncluded = "0";
                            }
                            #endregion

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
        public IActionResult DeleteAttendances()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> DeleteAttendances(int year, int month, long projectId)
        {
            var userList = await _userRepository.GetUserListByProjectIdAsync(projectId);
            var userNationalCodeList = userList.Where(x => x.NationalCode != null).Select(x => x.NationalCode).ToList();
            var listOfAttendances = _repository.GetUserAttendanceListByUserList(year.ToString(), month.ToString(), userNationalCodeList);

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