using Domain.Entities.Base.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Web.Abstractions;
using Web.Models;

namespace Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : BasePageModel<LoginModel>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager,
            ILogger<LoginModel> logger,
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                string userName = "";

                ApplicationUser user = null;

                if (Input.Email != null && IsValidEmail(Input.Email))
                {
                    user = await _userManager.FindByEmailAsync(Input.Email);

                    if (user != null && user.UserType == Common.Enums.UserType.SystemUser)
                    {
                        userName = user.UserName;
                    }
                }
                else if (Input.Username != null)
                {
                    user = await _userManager.FindByNameAsync(Input.Username);

                    if (user != null)
                    {
                        userName = user.UserName;
                    }
                }

                if (user != null)
                {
                    if (!user.IsActive)
                    {
                        return RedirectToPage("./Deactivated");
                    }
                    //else if (!user.EmailConfirmed)
                    //{
                    //    _notyf.Error("ایمیل تایید نشده است.");

                    //    ModelState.AddModelError(string.Empty, "Email Not Confirmed.");

                    //    return Page();
                    //}
                    else
                    {
                        var result = await _signInManager.PasswordSignInAsync(userName, Input.Password, Input.RememberMe, lockoutOnFailure: false);

                        if (result.Succeeded)
                        {
                            //await _mediator.Send(new AddActivityLogCommand() { userId = user.Id, Action = "Logged In" });
                            _logger.LogInformation("User logged in.");

                            _notyf.Success($"کاربر {userName} با موفقیت به سیستم وارد شدید.");

                            Response.Cookies.Append(
                                                        CookieRequestCultureProvider.DefaultCookieName,
                                                        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture("fa")),
                                                        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });

                            return LocalRedirect(returnUrl);
                        }
                        //await _mediator.Send(new AddActivityLogCommand() { userId = user.Id, Action = "Log-In Failed" });
                        if (result.RequiresTwoFactor)
                        {
                            return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                        }
                        if (result.IsLockedOut)
                        {
                            _notyf.Warning("اکانت شما مسدود شده است.");

                            _logger.LogWarning("User account locked out.");

                            return RedirectToPage("./Lockout");
                        }
                        else
                        {
                            _notyf.Error("اطلاعات ورود اشتباه است.");

                            ModelState.AddModelError(string.Empty, "Invalid login attempt.");

                            return Page();
                        }
                    }
                }
                else
                {
                    _notyf.Error("نام کاربری / ایمیل یافت نشد.");

                    ModelState.AddModelError(string.Empty, "Email / Username Not Found.");
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        public bool IsValidEmail(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}