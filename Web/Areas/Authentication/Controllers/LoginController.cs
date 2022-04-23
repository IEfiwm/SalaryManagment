using Application.Extensions;
using Domain.Entities.Base.Identity;
using Infrastructure.Repositories.Application.Idenitity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Areas.Authentication.Models;

namespace Web.Areas.Authentication.Controllers
{
    [AllowAnonymous]
    [Area("Authentication")]
    public class LoginController : BaseController<LoginController>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthenticationCodeRepository _authenticationCodeRepository;

        public LoginController(IAuthenticationCodeRepository authenticationCodeRepository,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _authenticationCodeRepository = authenticationCodeRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RequestVerificationCode(RequestVerificationCodeViewModel model)
        {
            var user = _userManager.Users.Where(m => m.PhoneNumber == model.Phone && m.UserType == Common.Enums.UserType.SystemUser).FirstOrDefault();

            if (user == null)
            {
                _notify.Error($"کاربر با این شماره تلفن در سیستم یافت نشد.");

                return View("Index");
            }
            if (user.IsBlocked)
            {
                _notify.Error($"کاربر مسدود شده است.");

                return View("Index");
            }
            if (!user.IsActive)
            {
                _notify.Error($"کاربر فعال نمی باشد.");

                return View("Index");
            }

            var code = await _authenticationCodeRepository.GenerateNewCode(model.Phone);

            if (code == "")
                _notify.Warning($"کد ورود قبلی هنوز برای شما معتبر می باشد.");
            else if (code == "-1")
            {
                _notify.Error($"کد ورود بیش از حد برای شما ارسال شده ساعتی دیگر دوباره امتحان کنید.");

                return View("Index");
            }
            else
            {
                SMSProvider.SendOTPCode(model.Phone, code);

                _notify.Success($"کد ورود با موفقیت ارسال شد.");
            }

            return View("VerifyPhoneNumber", new VerifyPhoneNumberViewModel { Phone = user.PhoneNumber });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ReSendVerificationCode(string Phone)
        {
            var code = await _authenticationCodeRepository.GenerateNewCode(Phone);

            if (code == "")
                return BadRequest($"کد ورود بیش از حد برای شما ارسال شده ساعتی دیگر دوباره امتحان کنید.");
            else if (code == "-1")
                return Ok($"کد ورود قبلی هنوز برای شما معتبر می باشد.");
            else if (code == "-2")
                return BadRequest($"از زمان درخواست قبلی شما مدت زمان تعیین شده نگذشته است.");
            else
            {
                SMSProvider.SendOTPCode(Phone, code);
                return Ok($"کد ورود با موفقیت ارسال شد.");
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (await _authenticationCodeRepository.VerifyCode(model.Phone, model.Code))
            {
                var user = _userManager.Users.Where(m => m.PhoneNumber == model.Phone && m.UserType == Common.Enums.UserType.SystemUser).FirstOrDefault();

                if (user == null)
                {
                    _notify.Error($"کاربر با این شماره تلفن در سیستم یافت نشد.");

                    return RedirectToAction("Index");
                }

                await _signInManager.SignInAsync(user, true);

                Response.Cookies.Append(
                            CookieRequestCultureProvider.DefaultCookieName,
                            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("fa")),
                            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            }
            else
            {
                _notify.Error($"کد صحیح نمی باشد.");

                return View("VerifyPhoneNumber", new VerifyPhoneNumberViewModel { Phone = model.Phone });
            }

            return Redirect("~/dashboard/home/index");
        }
    }
}