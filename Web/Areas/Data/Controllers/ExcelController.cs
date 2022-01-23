using Application.Enums;
using Application.Extensions;
using Application.Extensions.DataConversion;
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

                            var model = new Imported();

                            #region Validaition
                            decimal PersonnelCode;

                            if (string.IsNullOrEmpty(row?.GetCell(0)?.ToString()))
                                continue;
                            if (!DataConversion.Convert<decimal>(row?.GetCell(0)?.ToString(), out PersonnelCode))
                            {
                                _notify.Error("قالب داده صحیح نیست : کد پرسنلی ردیف: " + j);
                                return false;
                            }
                            model.PersonnelCode = PersonnelCode.ToString();
                            //string Name;
                            //if (!DataConversion.Convert<string>(row?.GetCell(1)?.ToString(), out Name))
                            //{
                            //    _notify.Error("قالب داده صحیح نیست : نام ردیف: " + j);
                            //    return false;
                            //}
                            model.Name = row?.GetCell(1)?.ToString();
                            //string FamilyName;
                            //if (!DataConversion.Convert<string>(row?.GetCell(2)?.ToString(), out FamilyName))
                            //{
                            //    _notify.Error("قالب داده صحیح نیست : نام خانوادگی ردیف: " + j);
                            //    return false;
                            //}
                            model.FamilyName = row?.GetCell(2)?.ToString();
                            //string JobTitle;
                            //if (!DataConversion.Convert<string>(row?.GetCell(3)?.ToString(), out JobTitle))
                            //{
                            //    _notify.Error("قالب داده صحیح نیست : شغل ردیف: " + j);
                            //    return false;
                            //}
                            model.JobTitle = row?.GetCell(3)?.ToString();
                            //string Province;
                            //if (!DataConversion.Convert<string>(row?.GetCell(4)?.ToString(), out Province))
                            //{
                            //    _notify.Error("قالب داده صحیح نیست : استان ردیف: " + j);
                            //    return false;
                            //}
                            model.Province = row?.GetCell(4)?.ToString();
                            //string AccountNumber;
                            //if (!DataConversion.Convert<string>(row?.GetCell(5)?.ToString(), out AccountNumber))
                            //{
                            //    _notify.Error("قالب داده صحیح نیست : شماره حساب ردیف: " + j);
                            //    return false;
                            //}
                            model.AccountNumber = row?.GetCell(5)?.ToString();
                            //string NationalCode;
                            //if (!DataConversion.Convert<string>(row?.GetCell(6)?.ToString(), out NationalCode))
                            //{
                            //    _notify.Error("قالب داده صحیح نیست : کدملی ردیف: " + j);
                            //    return false;
                            //}
                            model.NationalCode = row?.GetCell(6)?.ToString();
                            //string InsuranceNumber;
                            //if (!DataConversion.Convert<string>(row?.GetCell(7)?.ToString(), out InsuranceNumber))
                            //{
                            //    _notify.Error("قالب داده صحیح نیست : شماره بیمه ردیف: " + j);
                            //    return false;
                            //}
                            model.InsuranceNumber = row?.GetCell(7)?.ToString();
                            //string ServiceLocation;
                            //if (!DataConversion.Convert<string>(row?.GetCell(8)?.ToString(), out ServiceLocation))
                            //{
                            //    _notify.Error("قالب داده صحیح نیست : محل خدمت ردیف: " + j);
                            //    return false;
                            //}
                            model.ServiceLocation = row?.GetCell(8)?.ToString();
                            //string DegreeEducation;
                            //if (!DataConversion.Convert<string>(row?.GetCell(9)?.ToString(), out DegreeEducation))
                            //{
                            //    _notify.Error("قالب داده صحیح نیست : تحصیلات ردیف: " + j);
                            //    return false;
                            //}
                            model.DegreeEducation = row?.GetCell(9)?.ToString();
                            int DurationOperation;
                            if (!string.IsNullOrEmpty(row?.GetCell(10)?.ToString()))
                            {
                                if (!DataConversion.Convert<int>(row?.GetCell(10)?.ToString(), out DurationOperation))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارکرد موثر ردیف: " + j);
                                    return false;
                                }
                                model.DurationOperation = row?.GetCell(10)?.ToString();
                            }
                            else
                            {
                                model.DurationOperation = "0";
                            }
                            int OvertimeworkingTime;
                            if (!string.IsNullOrEmpty(row?.GetCell(11)?.ToString()))
                            {

                                if (!DataConversion.Convert<int>(row?.GetCell(11)?.ToString(), out OvertimeworkingTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارکرد اضافه کاری ردیف: " + j);
                                    return false;
                                }
                                model.OvertimeworkingTime = row?.GetCell(11)?.ToString();
                            }
                            else
                            {
                                model.OvertimeworkingTime = "0";
                            }
                            int NightworkingTime;
                            if (!string.IsNullOrEmpty(row?.GetCell(12)?.ToString()))
                            {

                                if (!DataConversion.Convert<int>(row?.GetCell(12)?.ToString(), out NightworkingTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارکرد شبکاری ردیف: " + j);
                                    return false;
                                }
                                model.NightworkingTime = row?.GetCell(12)?.ToString();
                            }
                            else
                            {
                                model.NightworkingTime = "0";
                            }
                            int HolidayworkingTime;
                            if (!string.IsNullOrEmpty(row?.GetCell(13)?.ToString()))
                            {

                                if (!DataConversion.Convert<int>(row?.GetCell(13)?.ToString(), out HolidayworkingTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارکرد تعطیل کاری ردیف: " + j);
                                    return false;
                                }
                                model.HolidayworkingTime = row?.GetCell(13)?.ToString();
                            }
                            else
                            {
                                model.HolidayworkingTime = "0";
                            }
                            if (!string.IsNullOrEmpty(row?.GetCell(14)?.ToString()))
                            {

                                int MissionTime;
                                if (!DataConversion.Convert<int>(row?.GetCell(14)?.ToString(), out MissionTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارکرد ماموریت ردیف: " + j);
                                    return false;
                                }
                                model.MissionTime = row?.GetCell(14)?.ToString();
                            }
                            else
                            {
                                model.MissionTime = "0";
                            }
                            int FoodTime;
                            if (!string.IsNullOrEmpty(row?.GetCell(15)?.ToString()))
                            {

                                if (!DataConversion.Convert<int>(row?.GetCell(15)?.ToString(), out FoodTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : كاركرد حق غذا ردیف: " + j);
                                    return false;
                                }
                                model.FoodTime = row?.GetCell(15)?.ToString();
                            }
                            else
                            {
                                model.FoodTime = "0";
                            }
                            int NumberChildren;
                            if (!string.IsNullOrEmpty(row?.GetCell(16)?.ToString()))
                            {
                                if (!DataConversion.Convert<int>(row?.GetCell(16)?.ToString(), out NumberChildren))
                                {
                                    _notify.Error("قالب داده صحیح نیست : تعداد فرزندان ردیف: " + j);
                                    return false;
                                }
                                model.NumberChildren = row?.GetCell(16)?.ToString();
                            }
                            else
                            {
                                model.NumberChildren = "0";
                            }
                            decimal Salary;
                            if (!string.IsNullOrEmpty(row?.GetCell(17)?.ToString()))
                            {

                                if (!DataConversion.Convert<decimal>(row?.GetCell(17)?.ToString(), out Salary))
                                {
                                    _notify.Error("قالب داده صحیح نیست : دستمزد ردیف: " + j);
                                    return false;
                                }
                                model.Salary = row?.GetCell(17)?.ToString();
                            }
                            else
                            {
                                model.Salary = "0";
                            }
                            decimal SeveranceDaily;
                            if (!string.IsNullOrEmpty(row?.GetCell(18)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(18)?.ToString(), out SeveranceDaily))
                                {
                                    _notify.Error("قالب داده صحیح نیست : پایه سنوات ردیف: " + j);
                                    return false;
                                }
                                model.SeveranceDaily = row?.GetCell(18)?.ToString();
                            }
                            else
                            {
                                model.SeveranceDaily = "0";
                            }
                            decimal DailyPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(19)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(19)?.ToString(), out DailyPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مزد روزانه ردیف: " + j);
                                    return false;
                                }
                                model.DailyPay = row?.GetCell(19)?.ToString();
                            }
                            else
                            {
                                model.DailyPay = "0";
                            }
                            decimal FoodAndHousingRight;
                            if (!string.IsNullOrEmpty(row?.GetCell(20)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(20)?.ToString(), out FoodAndHousingRight))
                                {
                                    _notify.Error("قالب داده صحیح نیست : حق خواربار و مسکن ردیف: " + j);
                                    return false;
                                }
                                model.FoodAndHousingRight = row?.GetCell(20)?.ToString();
                            }
                            else
                            {
                                model.FoodAndHousingRight = "0";
                            }
                            decimal WorkerRight;
                            if (!string.IsNullOrEmpty(row?.GetCell(21)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(21)?.ToString(), out WorkerRight))
                                {
                                    _notify.Error("قالب داده صحیح نیست : بن کارگر ردیف: " + j);
                                    return false;
                                }
                                model.WorkerRight = row?.GetCell(21)?.ToString();
                            }
                            else
                            {
                                model.WorkerRight = "0";
                            }
                            decimal OvertimePay;
                            if (!string.IsNullOrEmpty(row?.GetCell(22)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(22)?.ToString(), out OvertimePay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : اضافه کاری ثابت ردیف: " + j);
                                    return false;
                                }
                                model.OvertimePay = row?.GetCell(22)?.ToString();
                            }
                            else
                            {
                                model.OvertimePay = "0";
                            }
                            decimal MonthlyPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(23)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(23)?.ToString(), out MonthlyPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : سنوات ردیف: " + j);
                                    return false;
                                }
                                model.MonthlyPay = row?.GetCell(23)?.ToString();
                            }
                            else
                            {
                                model.MonthlyPay = "0";
                            }
                            decimal OvertimeworkingPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(24)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(24)?.ToString(), out OvertimeworkingPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : اضافه کاری ردیف: " + j);
                                    return false;
                                }
                                model.OvertimeworkingPay = row?.GetCell(24)?.ToString();
                            }
                            else
                            {
                                model.OvertimeworkingPay = "0";
                            }
                            decimal ChildrenRightPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(25)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(25)?.ToString(), out ChildrenRightPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : حق اولاد ردیف: " + j);
                                    return false;
                                }
                                model.ChildrenRightPay = row?.GetCell(25)?.ToString();
                            }
                            else
                            {
                                model.ChildrenRightPay = "0";
                            }
                            decimal HouseRightPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(26)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(26)?.ToString(), out HouseRightPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : حق مسکن ردیف: " + j);
                                    return false;
                                }
                                model.HouseRightPay = row?.GetCell(26)?.ToString();
                            }
                            else
                            {
                                model.HouseRightPay = "0";
                            }
                            decimal WorkerRightPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(27)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(27)?.ToString(), out WorkerRightPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : بن کارگری ردیف: " + j);
                                    return false;
                                }
                                model.WorkerRightPay = row?.GetCell(27)?.ToString();
                            }
                            else
                            {
                                model.WorkerRightPay = "0";
                            }
                            decimal NightworkingPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(28)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(28)?.ToString(), out NightworkingPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : شب کاری ردیف: " + j);
                                    return false;
                                }
                                model.NightworkingPay = row?.GetCell(28)?.ToString();
                            }
                            else
                            {
                                model.NightworkingPay = "0";
                            }
                            decimal HolidayworkingPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(29)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(29)?.ToString(), out HolidayworkingPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : تعطیل کاری ردیف: " + j);
                                    return false;
                                }
                                model.HolidayworkingPay = row?.GetCell(29)?.ToString();
                            }
                            else
                            {
                                model.HolidayworkingPay = "0";
                            }
                            decimal MissionPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(30)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(30)?.ToString(), out MissionPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : ماموریت ردیف: " + j);
                                    return false;
                                }
                                model.MissionPay = row?.GetCell(30)?.ToString();
                            }
                            else
                            {
                                model.MissionPay = "0";
                            }
                            decimal FoodPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(31)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(31)?.ToString(), out FoodPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : هزینه غذا ردیف: " + j);
                                    return false;
                                }
                                model.FoodPay = row?.GetCell(31)?.ToString();
                            }
                            else
                            {
                                model.FoodPay = "0";
                            }
                            decimal Other01;
                            if (!string.IsNullOrEmpty(row?.GetCell(32)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(32)?.ToString(), out Other01))
                                {
                                    _notify.Error("قالب داده صحیح نیست : سایر1 ردیف: " + j);
                                    return false;
                                }
                                model.Other01 = row?.GetCell(32)?.ToString();
                            }
                            else
                            {
                                model.Other01 = "0";
                            }
                            decimal Other02;
                            if (!string.IsNullOrEmpty(row?.GetCell(33)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(33)?.ToString(), out Other02))
                                {
                                    _notify.Error("قالب داده صحیح نیست :سایر2 ردیف: " + j);
                                    return false;
                                }
                                model.Other02 = row?.GetCell(33)?.ToString();
                            }
                            else
                            {
                                model.Other02 = "0";
                            }
                            decimal Disparity;
                            if (!string.IsNullOrEmpty(row?.GetCell(34)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(34)?.ToString(), out Disparity))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مابتفاوت ردیف: " + j);
                                    return false;
                                }
                                model.Disparity = row?.GetCell(34)?.ToString();
                            }
                            else
                            {
                                model.Disparity = "0";
                            }
                            decimal PreviousReceipt;
                            if (!string.IsNullOrEmpty(row?.GetCell(35)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(35)?.ToString(), out PreviousReceipt))
                                {
                                    _notify.Error("قالب داده صحیح نیست : طلب از قبل ردیف: " + j);
                                    return false;
                                }
                                model.PreviousReceipt = row?.GetCell(35)?.ToString();
                            }
                            else
                            {
                                model.PreviousReceipt = "0";
                            }
                            decimal SumSalaryAndBenefit;
                            if (!string.IsNullOrEmpty(row?.GetCell(36)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(36)?.ToString(), out SumSalaryAndBenefit))
                                {
                                    _notify.Error("قالب داده صحیح نیست : جمع حقوق و مزایا ردیف: " + j);
                                    return false;
                                }
                                model.SumSalaryAndBenefit = row?.GetCell(36)?.ToString();
                            }
                            else
                            {
                                model.SumSalaryAndBenefit = "0";
                            }
                            decimal TaxationPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(37)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(37)?.ToString(), out TaxationPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مشمول مالیات ردیف: " + j);
                                    return false;
                                }
                                model.TaxationPay = row?.GetCell(37)?.ToString();
                            }
                            else
                            {
                                model.TaxationPay = "0";
                            }
                            decimal InsurancePay;
                            if (!string.IsNullOrEmpty(row?.GetCell(38)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(38)?.ToString(), out InsurancePay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مشمول بیمه ردیف: " + j);
                                    return false;
                                }
                                model.InsurancePay = row?.GetCell(38)?.ToString();
                            }
                            else
                            {
                                model.InsurancePay = "0";
                            }
                            decimal Insurance7Percent;
                            if (!string.IsNullOrEmpty(row?.GetCell(39)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(39)?.ToString(), out Insurance7Percent))
                                {
                                    _notify.Error("قالب داده صحیح نیست : بیمه 7% ردیف: " + j);
                                    return false;
                                }
                                model.Insurance7Percent = row?.GetCell(39)?.ToString();
                            }
                            else
                            {
                                model.Insurance7Percent = "0";
                            }
                            decimal Taxation;
                            if (!string.IsNullOrEmpty(row?.GetCell(40)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(40)?.ToString(), out Taxation))
                                {
                                    _notify.Error("قالب داده صحیح نیست : ماليات ردیف: " + j);
                                    return false;
                                }
                                model.Taxation = row?.GetCell(40)?.ToString();
                            }
                            else
                            {
                                model.Taxation = "0";
                            }
                            decimal HelpPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(41)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(41)?.ToString(), out HelpPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مساعده ردیف: " + j);
                                    return false;
                                }
                                model.HelpPay = row?.GetCell(41)?.ToString();
                            }
                            else
                            {
                                model.HelpPay = "0";
                            }
                            decimal Absence;
                            if (!string.IsNullOrEmpty(row?.GetCell(42)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(42)?.ToString(), out Absence))
                                {
                                    _notify.Error("قالب داده صحیح نیست : غیبت ردیف: " + j);
                                    return false;
                                }
                                model.Absence = row?.GetCell(42)?.ToString();
                            }
                            else
                            {
                                model.Absence = "0";
                            }
                            decimal Debt;
                            if (!string.IsNullOrEmpty(row?.GetCell(43)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(43)?.ToString(), out Debt))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مبلغ بدهی از قبل ردیف: " + j);
                                    return false;
                                }
                                model.Debt = row?.GetCell(43)?.ToString();
                            }
                            else
                            {
                                model.Debt = "0";
                            }
                            decimal OtherDeductions;
                            if (!string.IsNullOrEmpty(row?.GetCell(44)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(44)?.ToString(), out OtherDeductions))
                                {
                                    _notify.Error("قالب داده صحیح نیست : سایر کسورات ردیف: " + j);
                                    return false;
                                }
                                model.OtherDeductions = row?.GetCell(44)?.ToString();
                            }
                            else
                            {
                                model.OtherDeductions = "0";
                            }
                            decimal SumDeductions;
                            if (!string.IsNullOrEmpty(row?.GetCell(45)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(45)?.ToString(), out SumDeductions))
                                {
                                    _notify.Error("قالب داده صحیح نیست : جمع كسورات ردیف: " + j);
                                    return false;
                                }
                                model.SumDeductions = row?.GetCell(45)?.ToString();
                            }
                            else
                            {
                                model.SumDeductions = "0";
                            }
                            decimal PureIncome;
                            if (!string.IsNullOrEmpty(row?.GetCell(46)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(46)?.ToString(), out PureIncome))
                                {
                                    _notify.Error("قالب داده صحیح نیست : خالص دریافتی ردیف: " + j);
                                    return false;
                                }
                                model.PureIncome = row?.GetCell(46)?.ToString();
                            }
                            else
                            {
                                model.PureIncome = "0";
                            }
                            decimal Year;
                            if (!string.IsNullOrEmpty(row?.GetCell(47)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(47)?.ToString(), out Year))
                                {
                                    _notify.Error("قالب داده صحیح نیست : سال ردیف: " + j);
                                    return false;
                                }
                                model.Year = row?.GetCell(47)?.ToString();
                            }
                            else
                            {
                                model.Year = "0";
                            }
                            decimal Month;
                            if (!string.IsNullOrEmpty(row?.GetCell(48)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(48)?.ToString(), out Month))
                                {
                                    _notify.Error("قالب داده صحیح نیست : ماه ردیف: " + j);
                                    return false;
                                }
                                model.Month = row?.GetCell(48)?.ToString();
                            }
                            else
                            {
                                model.Month = "0";
                            }
                            decimal SeveranceMonthly;
                            if (!string.IsNullOrEmpty(row?.GetCell(49)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(49)?.ToString(), out SeveranceMonthly))
                                {
                                    _notify.Error("قالب داده صحیح نیست : پایه سنوات مزایا ردیف: " + j);
                                    return false;
                                }
                                model.SeveranceMonthly = row?.GetCell(49)?.ToString();
                            }
                            else
                            {
                                model.SeveranceMonthly = "0";
                            }
                            int ShiftWorkTime;
                            if (!string.IsNullOrEmpty(row?.GetCell(50)?.ToString()))
                            {
                                if (!DataConversion.Convert<int>(row?.GetCell(50)?.ToString(), out ShiftWorkTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارکرد نوبت کاری کارکرد ردیف: " + j);
                                    return false;
                                }
                                model.ShiftWorkTime = row?.GetCell(50)?.ToString();
                            }
                            else
                            {
                                model.ShiftWorkTime = "0";
                            }
                            decimal ShiftWorkPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(51)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(51)?.ToString(), out ShiftWorkPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : نوبت کاری مزایا ردیف: " + j);
                                    return false;
                                }
                                model.ShiftWorkPay = row?.GetCell(51)?.ToString();
                            }
                            else
                            {
                                model.ShiftWorkPay = "0";
                            }
                            decimal SupplementaryInsuranceDeduction;
                            if (!string.IsNullOrEmpty(row?.GetCell(52)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(52)?.ToString(), out SupplementaryInsuranceDeduction))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کسر بیمه تکمیلی - کسورات ردیف: " + j);
                                    return false;
                                }
                                model.ShiftWorkPay = row?.GetCell(52)?.ToString();
                            }
                            else
                            {
                                model.ShiftWorkPay = "0";
                            }
                            int RewardTime;
                            if (!string.IsNullOrEmpty(row?.GetCell(53)?.ToString()))
                            {
                                if (!DataConversion.Convert<int>(row?.GetCell(53)?.ToString(), out RewardTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : پاداش - کارکرد ردیف: " + j);
                                    return false;
                                }
                                model.RewardTime = row?.GetCell(53)?.ToString();
                            }
                            else
                            {
                                model.RewardTime = "0";
                            }
                            decimal RewardPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(54)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(54)?.ToString(), out RewardPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : سنوات - مزایا  ردیف: " + j);
                                    return false;
                                }
                                model.RewardPay = row?.GetCell(54)?.ToString();
                            }
                            else
                            {
                                model.RewardPay = "0";
                            }
                            decimal YearsPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(55)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(55)?.ToString(), out YearsPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : عیدی - مزایا ردیف: " + j);
                                    return false;
                                }
                                model.YearsPay = row?.GetCell(55)?.ToString();
                            }
                            else
                            {
                                model.YearsPay = "0";
                            }
                            decimal FestalPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(56)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(56)?.ToString(), out FestalPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : پاداش - مزایا ردیف: " + j);
                                    return false;
                                }
                                model.FestalPay = row?.GetCell(56)?.ToString();
                            }
                            else
                            {
                                model.FestalPay = "0";
                            }
                            decimal BasicOverTimePay;
                            if (!string.IsNullOrEmpty(row?.GetCell(57)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(57)?.ToString(), out BasicOverTimePay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مبلغ اضافه کاری ثابت-مزایا ردیف: " + j);
                                    return false;
                                }
                                model.BasicOverTimePay = row?.GetCell(57)?.ToString();
                            }
                            else
                            {
                                model.BasicOverTimePay = "0";
                            }
                            decimal SupplementaryInsuranceSupervisor;
                            if (!string.IsNullOrEmpty(row?.GetCell(58)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(58)?.ToString(), out SupplementaryInsuranceSupervisor))
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
                            decimal SupplementaryInsuranceForDependents;
                            if (!string.IsNullOrEmpty(row?.GetCell(59)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(59)?.ToString(), out SupplementaryInsuranceForDependents))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کسر بیمه تکمیلی افراد تحت تکفل ردیف: " + j);
                                    return false;
                                }
                                model.SupplementaryInsuranceForDependents = row?.GetCell(59)?.ToString();
                            }
                            else
                            {
                                model.SupplementaryInsuranceForDependents = "0";
                            }
                            decimal NonDependentSupplementaryInsurance;
                            if (!string.IsNullOrEmpty(row?.GetCell(60)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(60)?.ToString(), out NonDependentSupplementaryInsurance))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کسر بیمه تکمیلی افرادغیر تحت تکفل ردیف: " + j);
                                    return false;
                                }
                                model.NonDependentSupplementaryInsurance = row?.GetCell(60)?.ToString();
                            }
                            else
                            {
                                model.NonDependentSupplementaryInsurance = "0";
                            }
                            decimal WelfareCostPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(61)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(61)?.ToString(), out WelfareCostPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : هزینه رفاهی ردیف: " + j);
                                    return false;
                                }
                                model.WelfareCostPay = row?.GetCell(61)?.ToString();
                            }
                            else
                            {
                                model.WelfareCostPay = "0";
                            }
                            decimal TransportationPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(62)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(62)?.ToString(), out TransportationPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : ایاب و ذهاب ردیف: " + j);
                                    return false;
                                }
                                model.TransportationPay = row?.GetCell(62)?.ToString();
                            }
                            else
                            {
                                model.TransportationPay = "0";
                            }
                            int DelayedTime;
                            if (!string.IsNullOrEmpty(row?.GetCell(63)?.ToString()))
                            {
                                if (!DataConversion.Convert<int>(row?.GetCell(63)?.ToString(), out DelayedTime))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارکرد معوقه ردیف: " + j);
                                    return false;
                                }
                                model.DelayedTime = row?.GetCell(63)?.ToString();
                            }
                            else
                            {
                                model.DelayedTime = "0";
                            }
                            decimal InstitutionaLoan;
                            if (!string.IsNullOrEmpty(row?.GetCell(64)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(64)?.ToString(), out InstitutionaLoan))
                                {
                                    _notify.Error("قالب داده صحیح نیست : وام موسسه ردیف: " + j);
                                    return false;
                                }
                                model.InstitutionaLoan = row?.GetCell(64)?.ToString();
                            }
                            else
                            {
                                model.InstitutionaLoan = "0";
                            }
                            decimal SamanLoan;
                            if (!string.IsNullOrEmpty(row?.GetCell(65)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(65)?.ToString(), out SamanLoan))
                                {
                                    _notify.Error("قالب داده صحیح نیست : وام سامان ردیف: " + j);
                                    return false;
                                }
                                model.SamanLoan = row?.GetCell(65)?.ToString();
                            }
                            else
                            {
                                model.SamanLoan = "0";
                            }
                            decimal DelayedTransportationPay;
                            if (!string.IsNullOrEmpty(row?.GetCell(66)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(66)?.ToString(), out DelayedTransportationPay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : معوقه ایاب و ذهاب ردیف: " + j);
                                    return false;
                                }
                                model.DelayedTransportationPay = row?.GetCell(66)?.ToString();
                            }
                            else
                            {
                                model.DelayedTransportationPay = "0";
                            }
                            decimal DelayedSupplementaryInsuranceDeduction;
                            if (!string.IsNullOrEmpty(row?.GetCell(67)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(67)?.ToString(), out DelayedSupplementaryInsuranceDeduction))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کسر بیمه تکمیلی ماه معوقه ردیف: " + j);
                                    return false;
                                }
                                model.DelayedSupplementaryInsuranceDeduction = row?.GetCell(67)?.ToString();
                            }
                            else
                            {
                                model.DelayedSupplementaryInsuranceDeduction = "0";
                            }
                            decimal WelfareAllowancePay;
                            if (!string.IsNullOrEmpty(row?.GetCell(68)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(68)?.ToString(), out WelfareAllowancePay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کمک هزینه رفاهی ردیف: " + j);
                                    return false;
                                }
                                model.WelfareAllowancePay = row?.GetCell(68)?.ToString();
                            }
                            else
                            {
                                model.WelfareAllowancePay = "0";
                            }
                            decimal PerformancePay;
                            if (!string.IsNullOrEmpty(row?.GetCell(69)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(69)?.ToString(), out PerformancePay))
                                {
                                    _notify.Error("قالب داده صحیح نیست : کارایی ردیف: " + j);
                                    return false;
                                }
                                model.PerformancePay = row?.GetCell(69)?.ToString();
                            }
                            else
                            {
                                model.PerformancePay = "0";
                            }
                            decimal MonthlyBenefits;
                            if (!string.IsNullOrEmpty(row?.GetCell(70)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(70)?.ToString(), out MonthlyBenefits))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مزایای مشمول ردیف: " + j);
                                    return false;
                                }
                                model.MonthlyBenefits = row?.GetCell(70)?.ToString();
                            }
                            else
                            {
                                model.MonthlyBenefits = "0";
                            }
                            decimal MonthlyWagesAndBenefitsIncluded;
                            if (!string.IsNullOrEmpty(row?.GetCell(71)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(71)?.ToString(), out MonthlyWagesAndBenefitsIncluded))
                                {
                                    _notify.Error("قالب داده صحیح نیست : دستمزد و مزایای مشمول ماهانه ردیف: " + j);
                                    return false;
                                }
                                model.MonthlyWagesAndBenefitsIncluded = row?.GetCell(71)?.ToString();
                            }
                            else
                            {
                                model.MonthlyWagesAndBenefitsIncluded = "0";
                            }
                            decimal IncludedAndNotIncluded;
                            if (!string.IsNullOrEmpty(row?.GetCell(72)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(72)?.ToString(), out IncludedAndNotIncluded))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مشمول و غیر مشمول ردیف: " + j);
                                    return false;
                                }
                                model.IncludedAndNotIncluded = row?.GetCell(72)?.ToString();
                            }
                            else
                            {
                                model.IncludedAndNotIncluded = "0";
                            }
                            decimal UnemploymentInsurance;
                            if (!string.IsNullOrEmpty(row?.GetCell(73)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(73)?.ToString(), out UnemploymentInsurance))
                                {
                                    _notify.Error("قالب داده صحیح نیست : حق بیمه بیکاری ردیف: " + j);
                                    return false;
                                }
                                model.UnemploymentInsurance = row?.GetCell(73)?.ToString();
                            }
                            else
                            {
                                model.UnemploymentInsurance = "0";
                            }
                            decimal Insurance30Percent;
                            if (!string.IsNullOrEmpty(row?.GetCell(74)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(74)?.ToString(), out Insurance30Percent))
                                {
                                    _notify.Error("قالب داده صحیح نیست : جمع کل حق بیمه اعم از حق بیمه و بیمه بیکاری ردیف: " + j);
                                    return false;
                                }
                                model.Insurance30Percent = row?.GetCell(74)?.ToString();
                            }
                            else
                            {
                                model.Insurance30Percent = "0";
                            }
                            decimal EmployerShareInsurance;
                            if (!string.IsNullOrEmpty(row?.GetCell(75)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(75)?.ToString(), out EmployerShareInsurance))
                                {
                                    _notify.Error("قالب داده صحیح نیست : حق بیمه سهم کارفرما ردیف: " + j);
                                    return false;
                                }
                                model.EmployerShareInsurance = row?.GetCell(75)?.ToString();
                            }
                            else
                            {
                                model.EmployerShareInsurance = "0";
                            }
                            decimal? ContinuousBasicRightsToHousingAndChildrenRights;
                            if (!string.IsNullOrEmpty(row?.GetCell(76)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal?>(row?.GetCell(76)?.ToString(), out ContinuousBasicRightsToHousingAndChildrenRights))
                                {
                                    _notify.Error("قالب داده صحیح نیست : مستمر - حقوق پایه بن و مسکن و حق اولاد ردیف: " + j);
                                    return false;
                                }
                                model.ContinuousBasicRightsToHousingAndChildrenRights = row?.GetCell(76)?.ToString();
                            }
                            else
                            {
                                model.ContinuousBasicRightsToHousingAndChildrenRights = "0";
                            }
                            decimal ContinuousBaseSalaryAndBaseYears;
                            if (!string.IsNullOrEmpty(row?.GetCell(77)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(77)?.ToString(), out ContinuousBaseSalaryAndBaseYears))
                                {
                                    _notify.Error("قالب داده صحیح نیست : معوقه حقوق پایه و پایه سنوات ردیف: " + j);
                                    return false;
                                }
                                model.ContinuousBaseSalaryAndBaseYears = row?.GetCell(77)?.ToString();
                            }
                            else
                            {
                                model.ContinuousBaseSalaryAndBaseYears = "0";
                            }
                            decimal NonContinuousIncludedNotIncluded;
                            if (!string.IsNullOrEmpty(row?.GetCell(78)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(78)?.ToString(), out NonContinuousIncludedNotIncluded))
                                {
                                    _notify.Error("قالب داده صحیح نیست : غیر مستمر - مشمول و غیر مشمول ردیف: " + j);
                                    return false;
                                }
                                model.NonContinuousIncludedNotIncluded = row?.GetCell(78)?.ToString();
                            }
                            else
                            {
                                model.NonContinuousIncludedNotIncluded = "0";
                            }
                            decimal NonContinuousIncluded;
                            if (!string.IsNullOrEmpty(row?.GetCell(79)?.ToString()))
                            {
                                if (!DataConversion.Convert<decimal>(row?.GetCell(79)?.ToString(), out NonContinuousIncluded))
                                {
                                    _notify.Error("قالب داده صحیح نیست : غیر مستمر - مشمول ردیف: " + j);
                                    return false;
                                }
                                model.NonContinuousIncluded = row?.GetCell(79)?.ToString();
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