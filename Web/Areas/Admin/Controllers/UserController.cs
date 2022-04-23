﻿using Application.Enums;
using Common.Enums;
using Common.Helpers;
using Common.Models.DataTable;
using Domain.Entities.Base.Identity;
using Domain.Entities.Basic;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Application.Basic;
using Infrastructure.Repositories.Application.Idenitity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OfficeOpenXml;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Areas.Admin.Models;
using Web.Areas.Dashboard.Models;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin,Manager")]
    public class UserController : BaseController<UserController>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserRepository _userRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly IAdditionalUserDateRepository _additionalUserDateRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IBankRepository _bankRepository;
        private readonly IUser_RoleRepository _user_RoleRepository;

        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<ApplicationRole> roleManager,
            IUserRepository userRepository,
            IBankAccountRepository bankAccountRepository,
            IAdditionalUserDateRepository additionalUserDateRepository,
            IDocumentRepository documentRepository,
            IFileRepository fileRepository,
            IBankRepository bankRepository,
            IUser_RoleRepository user_RoleRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _userRepository = userRepository;
            _bankAccountRepository = bankAccountRepository;
            _additionalUserDateRepository = additionalUserDateRepository;
            _documentRepository = documentRepository;
            _fileRepository = fileRepository;
            _bankRepository = bankRepository;
            _user_RoleRepository = user_RoleRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Policy = Permissions.Users.View)]
        public IActionResult Personnel()
        {
            return View();
        }

        public async Task<IActionResult> ViewAll()
        {
            var allUsersExceptCurrentUser = await _userRepository.GetSysUserListAsync();

            var allUsers = new List<ApplicationUser>();

            allUsersExceptCurrentUser.ForEach(m =>
            {
                var rolesTask = _userManager.GetRolesAsync(m);

                rolesTask.ContinueWith(r =>
                {
                    if (!r.Result.Contains(Roles.SuperAdmin.ToString()))
                        allUsers.Add(m);
                });

                rolesTask.Wait();
            });

            var model = _mapper.Map<List<UserViewModel>>(allUsers);

            return PartialView("_ViewAll", model);
        }

        public async Task<IActionResult> LoadAll(long projectId, string key, byte pageSize, byte pageNumber, EmployeeStatus? employeeStatus, Gender? gender, MilitaryService? militaryService, MaritalStatus? maritalStatus)
        {

            var model = await FilterUsers(projectId, key, pageSize, pageNumber, employeeStatus, gender, militaryService, maritalStatus);
            return PartialView("_LoadAll", model);
        }

        public async Task<IActionResult> ExportExcel(long projectId, string key, EmployeeStatus? employeeStatus, Gender? gender, MilitaryService? militaryService, MaritalStatus? maritalStatus)
        {

            var model = await FilterUsers(projectId, key, int.MaxValue, 0, employeeStatus, gender, militaryService, maritalStatus);

            MemoryStream result = (MemoryStream)ExportToExcel(model.ViewModel);
            // Set memorystream position; if we don't it'll fail
            result.Position = 0;

            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Personnel.xlsx");

        }

        public async Task<IActionResult> Create()
        {
            ViewData["RoleList"] = _mapper.Map<List<RoleViewModel>>(await _roleManager.Roles.ToListAsync());
            return View("Create", new UserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                //MailAddress address = new MailAddress(userModel.Email);
                //string userName = address.User;
                var user = new ApplicationUser
                {
                    UserName = userModel.UserName,
                    Email = userModel.Email,
                    FirstName = userModel.FirstName,
                    LastName = userModel.LastName,
                    EmailConfirmed = true,
                    IsActive = true,
                    UserType = Common.Enums.UserType.SystemUser
                };

                var result = await _userManager.CreateAsync(user, userModel.Password);


                if (result.Succeeded)
                {
                    //add role
                    if (!string.IsNullOrEmpty(userModel.RoleId))
                    {
                        var resRole = await _user_RoleRepository.InsertAndSaveAsync(new IdentityUserRole<string>
                        {
                            UserId = user.Id,
                            RoleId = userModel.RoleId
                        });

                        if (resRole == 0)
                        {
                            _notify.Error("عملیات ثبت کاربر با خطا مواجعه شد.");
                            return RedirectToAction("Index");
                        }
                    }

                    _notify.Success($"کاربر {userModel.UserName} با موفقیت اضافه شد.");

                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    _notify.Error(error.Description);
                }
                ViewData["RoleList"] = _mapper.Map<List<RoleViewModel>>(await _roleManager.Roles.ToListAsync());
                return View("Create", userModel);
            }

            return default;
        }

        public async Task<IActionResult> Edit(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var model = _mapper.Map<UserViewModel>(user);
            var user_role = await _user_RoleRepository.GetByUserId(userId);

            if (user_role != null && user_role.Count > 0)
            {
                model.RoleId = user_role.FirstOrDefault().RoleId;
            }

            ViewData["RoleList"] = _mapper.Map<List<RoleViewModel>>(await _roleManager.Roles.ToListAsync());

            return View("Edit", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserViewModel userModel)
        {
            if (ModelState.IsValid)
            {
                //MailAddress address = new MailAddress(userModel.Email);
                //string userName = address.User;
                var ouser = await _userManager.FindByIdAsync(userModel.Id);

                ouser = _mapper.Map<UserViewModel, ApplicationUser>(userModel, ouser);
                ouser.ProjectRef = null;
                var result = await _userManager.UpdateAsync(ouser);

                if (!string.IsNullOrEmpty(userModel.RoleId))
                {
                    //delete by userId
                    var user_role = await _user_RoleRepository.GetByUserId(userModel.Id);

                    if (user_role != null && user_role.Count > 0)
                    {
                        foreach (var roleMenu in user_role)
                        {
                            await _user_RoleRepository.DeleteAsync(roleMenu);
                        }
                    }

                    //add role
                    var resRole = await _user_RoleRepository.InsertAndSaveAsync(new IdentityUserRole<string>
                    {
                        UserId = userModel.Id,
                        RoleId = userModel.RoleId
                    });

                    if (resRole == 0)
                    {
                        _notify.Error("عملیات ویرایش کاربر با خطا مواجعه شد.");
                        return RedirectToAction("Index");
                    }
                }

                _notify.Success($"کاربر {userModel.UserName} با موفقیت ویرایش شد.");

                return RedirectToAction("Index");
            }

            return default;
        }

        public async Task<IActionResult> EditPersonnel(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var model = _mapper.Map<UserViewModel>(user);
            if (user.BankAccountRef != null)
            {
                var acc = await _bankAccountRepository.GetByIdAsync(user.BankAccountRef.Value);
                model.BankAccNumber = acc.AccountNumber;
                model.BankName = acc.Title;
                model.BankId = acc.BankId;
            }
            model.AdditionalUserData = _mapper.Map<List<AdditionalUserDataViewModel>>
               (_additionalUserDateRepository.Model.Include(x => x.Documents).Where(x => x.ParentRef == user.Id)).ToList();

            if (!model.AdditionalUserData.Any(x => x.FamilyRole == Common.Enums.FamilyRole.Me))
                model.AdditionalUserData.Add(new AdditionalUserDataViewModel { FamilyRole = Common.Enums.FamilyRole.Me, ParentRef = user.Id });

            model.AdditionalUserData.ForEach(x =>
            {
                if (x.Documents == null)
                    x.Documents = new List<DocumentViewModel>();

            });

            var banks = await _bankRepository.GetListAsync();
            ViewData["banks"] = _mapper.Map<List<BankViewModel>>(banks.Where(x => x.Active).ToList());

            return View("EditPersonnel", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPersonnel(UserViewModel user)
        {
            long? bankRef = null;
            if (user.BankId != 0 && user.BankId is not null && user.BankAccNumber is not null)
            {
                var bank = await _bankRepository.GetByIdAsync(user.BankId.Value);

                bankRef = await _bankAccountRepository.InsertAndSaveAsync(new BankAccount
                {
                    AccountNumber = user.BankAccNumber,
                    Title = bank.Title,
                    iBan = user.ShebaNumber,
                    BankId = bank.Id,
                    CreatedByRef = (await _userManager.GetUserAsync(HttpContext.User)).Id,
                    CreatedDate = System.DateTime.Now
                });
            }
            user.UserName = user.PhoneNumber;

            var ouser = await _userRepository.GetUserByIdAsync(user.Id);

            ouser.BankAccountRef = bankRef;

            ouser = _mapper.Map<UserViewModel, ApplicationUser>(user, ouser);

            var res = await _userRepository.SaveChangesAsync();

            //var res = await _userManager.UpdateAsync(ouser);

            //save files
            foreach (var data in user.AdditionalUserData)
            {
                foreach (var doc in data.Documents)
                {
                    if (doc.File != null)
                    {
                        doc.Id = 0;
                        doc.FullPath = (await _fileRepository.SaveImageAsync(doc.File));
                    }
                }
            }

            var additionaluserModel = _mapper.Map<List<AdditionalUserData>>(user.AdditionalUserData);

            await _additionalUserDateRepository.UpdateByUserId(additionaluserModel, user.Id);


            if (res > 0)
                _notify.Success("ویرایش با موفقیت انجام شد.");
            else
            {
                _notify.Error("ویرایش انجام نشد.");

                return Redirect("/admin/user/editpersonnel?userId=" + user.Id);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> CreatePersonnel()
        {
            var banks = await _bankRepository.GetListAsync();
            ViewData["banks"] = _mapper.Map<List<BankViewModel>>(banks.Where(x => x.Active).ToList());

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePersonnel(UserViewModel user)
        {
            long? bankRef = null;
            if (user.BankId != 0 && user.BankId is not null && user.BankAccNumber is not null)
            {
                var bank = await _bankRepository.GetByIdAsync(user.BankId.Value);

                bankRef = await _bankAccountRepository.InsertAndSaveAsync(new BankAccount
                {
                    AccountNumber = user.BankAccNumber,
                    iBan = user.ShebaNumber,
                    Title = bank.Title,
                    BankId = bank.Id,
                    CreatedByRef = (await _userManager.GetUserAsync(HttpContext.User)).Id,
                    CreatedDate = System.DateTime.Now
                });
            }

            user.Id = Guid.NewGuid().ToString();

            user.UserName = user.PersonnelCode ?? user.NationalCode;

            if (_userRepository
                .Users.Where(m => m.UserName == user.UserName || m.PersonnelCode == user.PersonnelCode || m.NationalCode == user.NationalCode || m.PhoneNumber == user.PhoneNumber).FirstOrDefault() != null)
            {
                _notify.Error("افزودن کاربر انجام نشد.");

                _notify.Error("کاربر با این مشخصات در سیستم موجود می باشد.");

                return RedirectToAction("Index");
            }

            ApplicationUser model = _mapper.Map<ApplicationUser>(user);

            model.BankAccountRef = bankRef;

            model.CreateDate = System.DateTime.Now;

            model.IsDeleted = false;

            model.IsProfileCompleted = true;

            model.UserType = Common.Enums.UserType.PublicUser;

            var res = await _userManager.CreateAsync(model, model.PersonnelCode);

            //save files
            foreach (var data in user.AdditionalUserData)
            {
                data.ParentRef = user.Id;
                foreach (var doc in data.Documents)
                {
                    if (doc.File != null)
                    {
                        doc.Id = 0;
                        doc.FullPath = (await _fileRepository.SaveImageAsync(doc.File));
                    }
                }
            }

            var additionaluserModel = _mapper.Map<List<AdditionalUserData>>(user.AdditionalUserData);

            await _additionalUserDateRepository.CreateByUserId(additionaluserModel, user.Id);



            if (res.Succeeded)
            {
                await _userManager.AddToRoleAsync(model, Roles.User.ToString());

                _notify.Success("افزودن کاربر با موفقیت انجام شد.");
            }
            else
            {
                _notify.Error("افزودن کاربر انجام نشد.");

                if (user.Id is null)
                {
                    RedirectToAction("Index");
                }

                return Redirect("/admin/user/edit?userId=" + user.Id);
            }

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            user.IsDeleted = true;
            user.UserName = user.UserName + "_Deleted";
            user.NormalizedUserName = user.NormalizedUserName + "_DELETED";

            var res = await _userManager.UpdateAsync(user);

            if (res.Succeeded)
            {
                await _bankAccountRepository.SoftDeleteAsync(user.BankAccountRef.Value);

                await _bankAccountRepository.SaveChangesAsync();
            }
            else
                _notify.Error("حذف کاربر انجام نشد.");

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ContractList(string startDate, string endDate, long projectId = 0)
        {
            ViewData["projectId"] = projectId;
            var model = new ContractListViewModel();

            if (projectId != 0)
            {
                var usersByprojectId = await _userRepository.GetUserListByProjectIdAsync(projectId);

                model.Users = _mapper.Map<IEnumerable<UserViewModel>>(usersByprojectId).ToList();
            }

            model.StartDate = startDate;

            model.EndDate = endDate;

            model.ProjectId = projectId;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetAllContractsFile(ContractListParameters model)
        {
            var usersByprojectId = await _userRepository.GetUserListByProjectIdAsync(model.projectId);

            if (usersByprojectId.Count == 0)
            {
                _notify.Error("کاربری یافت نشد.");
                return RedirectToAction("contractlist");
            }

            model.usernameList = usersByprojectId.Select(X => X.NationalCode).ToList();

            var path = Path.Combine(
    Directory.GetCurrentDirectory(),
    "wwwroot", "AllContracts_" + DateTime.Now.Year + CommonHelper.GetTowDigits(DateTime.Now.Month) + ".zip");

            var memory = new MemoryStream();

            using (var httpClient = new HttpClient())
            {
                httpClient.Timeout = TimeSpan.FromHours(6);

                var json = JsonConvert.SerializeObject(model);

                var data = new StringContent(json, Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync($@"{ _configuration["Base:KoshaCore:APIAddress"].ToString()}/Contract/GetAll", data))
                {
                    var test = await response.Content.ReadAsByteArrayAsync();

                    await System.IO.File.WriteAllBytesAsync(path, test);

                    using (var stream = new FileStream(path, FileMode.Open))
                    {
                        await stream.CopyToAsync(memory);
                    }

                    memory.Position = 0;

                    System.IO.File.Delete(path);
                }
            }
            return File(memory, "application/zip", Path.GetFileName(path));
        }

        public IActionResult GetDocuments(string userId)
        {
            var userAdditional = _additionalUserDateRepository.GetUserAdditionalById(userId);
            if (userAdditional != null)
            {
                var docs = _mapper.Map<List<DocumentViewModel>>(_documentRepository.GetByUserId(userAdditional.Id));
                return new JsonResult(docs);
            }
            return new JsonResult(null);
        }

        public IActionResult GetAdditionalUsers(string userId)
        {
            var docs = _mapper.Map<List<AdditionalUserDataViewModel>>(_additionalUserDateRepository.GetByUserId(userId));
            return new JsonResult(docs);
        }

        public async Task<IActionResult> PayRollTipList(int year, int month, long projectId = 0)
        {
            ViewData["projectId"] = projectId;
            var model = new PayRollListViewModel();

            if (projectId != 0)
            {
                var usersByprojectId = await _userRepository.GetUserListByProjectIdAsync(projectId);

                model.Users = _mapper.Map<IEnumerable<UserViewModel>>(usersByprojectId).ToList();

            }

            model.Year = year;

            model.Month = month;

            model.ProjectId = projectId;

            return View(model);
        }

        public async Task<IActionResult> GeneratePayRoll()
        {
            return View("GeneratePayRoll");
        }

        [HttpPost]
        public async Task<IActionResult> GeneratePayRoll(int year, int month, long projectId)
        {
            var client = new RestClient($@"{ _configuration["Base:KoshaCore:APIAddress"].ToString()}/Calculation/GenerateSalary/{projectId}/{year}/{month}");

            client.Timeout = -1;

            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            if (response.IsSuccessful)
                _notify.Success("درخواست تولید فیش حقوقی ثبت شد.");
            else if (response.StatusCode == HttpStatusCode.NotAcceptable)
                _notify.Error("خطا! درخواستی در حال انجام است.");
            else
                _notify.Error("عملیات با خطا مواجه شد.");

            return View("GeneratePayRoll");
        }

        [HttpGet]
        public async Task<string> GetPersonnelCode(long projectId)
        {
            if (projectId == default(long))
                return "0";

            return await _userRepository.GetLastPersonnelCode(projectId);
        }

        private object ExportToExcel(IEnumerable<UserViewModel> model)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var excelFile = new ExcelPackage())
            {
                var NewModel = model.Select(x => new
                {
                    x.PersonnelCode,
                    Name = x.FirstName + " " + x.LastName,
                    x.ProjectName,
                    x.FatherName,
                    x.NationalCode,
                    x.InsuranceCode,
                    x.BankAccNumber,
                    x.PhoneNumber,
                    x.JobTitle,
                    HasDocument = x.HasDocument ? "دارد" : "ندارد",
                    HasAdditionalUser = x.HasAdditionalUser ? "دارد" : "ندارد",
                    HasAdditionalUserDocument = x.HasAdditionalUserDocument ? "دارد" : "ندارد",
                    IsActive = x.IsActive ? "فعال" : "غیر فعال"
                });

                var worksheet = excelFile.Workbook.Worksheets.Add("Personnel");
                worksheet.View.RightToLeft = true;

                worksheet.Cells["A1"].Value = "کد پ";
                worksheet.Cells["B1"].Value = "نام ‌و ‌نام‌خانوادگی";
                worksheet.Cells["C1"].Value = "پروژه";
                worksheet.Cells["D1"].Value = "نام پدر";
                worksheet.Cells["E1"].Value = "کد ملی";
                worksheet.Cells["F1"].Value = "شماره بیمه";
                worksheet.Cells["G1"].Value = "شماره حساب";
                worksheet.Cells["H1"].Value = "شماره تماس";
                worksheet.Cells["I1"].Value = "شغل";
                worksheet.Cells["J1"].Value = "وضعیت ‌مدارک";
                worksheet.Cells["K1"].Value = "وضعیت اطلاعات تحت تکفل";
                worksheet.Cells["L1"].Value = "وضعیت مدارک تحت تکفل";
                worksheet.Cells["M1"].Value = "وضعیت";

                worksheet.Cells["A2"].LoadFromCollection(Collection: NewModel, PrintHeaders: false, OfficeOpenXml.Table.TableStyles.Light13);

                var allCells = worksheet.Cells[1, 1, worksheet.Dimension.End.Row, worksheet.Dimension.End.Column];
                var cellFont = allCells.Style.Font;
                cellFont.SetFromFont("B Nazanin", 11);
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                MemoryStream result = new MemoryStream();
                result.Position = 0;

                excelFile.SaveAs(result);

                return result;
            }
        }

        private async Task<DataTableViewModel<IEnumerable<UserViewModel>>> FilterUsers(long projectId, string key, int pageSize, byte pageNumber, EmployeeStatus? employeeStatus, Gender? gender, MilitaryService? militaryService, MaritalStatus? maritalStatus)
        {
            var allUsersExceptCurrentUser = await _userRepository.GetUserListByProjectIdDataTableAsync(projectId, key, pageSize, pageNumber, employeeStatus, gender, militaryService, maritalStatus);

            var model = _mapper.Map<DataTableViewModel<IEnumerable<UserViewModel>>>(allUsersExceptCurrentUser);

            foreach (var user in model.ViewModel)
            {
                await Task.Run(async () =>
                {
                    user.HasAdditionalUser = await _additionalUserDateRepository.HasAdditionalUsersAsync(user.Id);

                    user.HasAdditionalUserDocument = await _additionalUserDateRepository.HasAdditionalUserDocumentAsync(user.Id);

                    user.HasDocument = await _additionalUserDateRepository.HasDocumentsAsync(user.Id);
                });
            }
            return model;
        }
    }
}