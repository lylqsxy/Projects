using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Cobra.App.Infrastructure.Contracts;
using Cobra.App.Infrastructure.Constants;
using Cobra.App.Infrastructure.Services;
using Cobra.Filters;
using Cobra.Infrastructure.Contracts;
using Cobra.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web.Routing;
using System.Threading;
using System;

namespace Cobra.Controllers
{
    public class AccountController : Controller
    {
        #region Private Members

        private IAccountService _accountService;
        private ILogService _logService;
        private AuthenticationService _authService;

        #endregion Private Members

        public AccountController(
            IAccountService accountService,
            ILogService logService,
            AuthenticationService authService)
        {
            _accountService = accountService;
            _logService = logService;
            _authService = authService;
        }



        #region Registration

        // GET: Account/Register
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (_authService.IsAuthenticated())
            {
                return RedirectToLocal("/Home/Index");
            }
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateCustomAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {

            var result = await _accountService.CreateUserLogin(model.Email, model.Password);

            if (result.Succeeded)
            {
                //_authService.PasswordSignIn(model.Email, model.Password, false, false);
                return Json(new { Success = true, Message = "", RedirectUrl = "/Home/Index" });
            }
            return Json(new { Success = false, Message = result.Errors, RedirectUrl = "" });
        }

        // GET: /Account/ConfirmEmail
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string etoken, string email)
        {
            if (string.IsNullOrWhiteSpace(etoken) && string.IsNullOrWhiteSpace(email))
            {
                return RedirectToLocal("/Account/ConfirmEmailFailure");
            }
            var result = await _accountService.ConfirmEmail(etoken, email);
            if (result.Succeeded)
            {
                return RedirectToLocal("/Account/Login");
            }
            return View("Error");
        }

        #endregion Register

        #region Login / External Login

        // GET: Account/Login
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (_authService.IsAuthenticated())
            {
                return RedirectToLocal("/Home/Index");
            }
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateCustomAntiForgeryToken]
        public async Task<JsonResult> Login(LoginViewModel model)
        {
            var signInStatus = await _authService.CustomPasswordSignIn(model.Email, model.Password, model.RememberMe);
            switch (signInStatus)
            {
                case CustomSignInStatus.Success:
                    //to do
                    return Json(new { Success = true, Message = "", RedirectUrl = "/Manage/Index" });
                case CustomSignInStatus.RequireEmailConfirm:
                    //to do
                    return Json(new { Success = false, Message = "Please confirm your email", RedirectUrl = "" });
                case CustomSignInStatus.Failure:
                    //to do
                    return Json(new { Success = false, Message = "Wrong email / password combination.", RedirectUrl = "" });
                case CustomSignInStatus.LockedOut:
                    //to do
                    return Json(new { Success = false, Message = "Your account have been lockout. Please contact support team.", RedirectUrl = "" });
                default:
                    return Json(new { Success = false, Message = "Server error, please try again later.", RedirectUrl = "/Home/Index" });
            }
        }

        // GET: /Account/ExternalLogin
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ExternalLogin()
        {
            var vm = new LoginViewModel();
            vm.ReturnUrl = "";
            return PartialView("_ExternalLogin", vm);
        }


        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { loginProvider = provider, ReturnUrl = returnUrl }));
        }

        // GET: /Account/ExternalLoginCallback
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await _authService.CustomGetExternalLoginInfo();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }
            var result = await _authService.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);

                case SignInStatus.LockedOut:
                    return View("Lockout");

                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });

                case SignInStatus.Failure:
                default:
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }
            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await _authService.CustomGetExternalLoginInfo();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var result = await _accountService.CreateExternalLogin(model.Email, info);
                if (result.Succeeded)
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty , error);
                    }
                }
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        #endregion Login / External Login

        #region Reset Password

        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        // POST: /Account/ForgetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateCustomAntiForgeryToken]
        public async Task<ActionResult> ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.ForgetPassword(model.Email);
                if (result.Succeeded)
                {
                    return Json(new
                    {
                        Success = true,
                        Email = model.Email,
                        MsgText = "Success email."
                    });
                }
                return Json(new
                {
                    Success = false,
                    MsgText = "Something went wrong.",
                    Errors = result.Errors
                });
            }
            var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
            return Json(new
            {
                Success = false,
                MsgText = "Something went wrong.",
                Errors = errorList,
                
            });
        }

        // GET: /Account/ResetPassword
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string token)
        {

            return token == null ? View("error") : View();
        }

        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateCustomAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(string token, ResetPasswordViewModel model)
        {
            #region Whitespace Validation

            //All other server side validation are being handled
            // by ViewModel data annotations.
            if (Regex.Match(model.Password, @"\s").Success)
            {
                string[] errorWhitespace = { "Please don't add any space(s) in the password." };
                return Json(new
                {
                    Success = false,
                    MsgText = "Model is not valid.",
                    Errors = errorWhitespace
                });
            }

            #endregion Whitespace Validation

            var errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
            if (ModelState.IsValid)
            {
                var result = await _accountService.ResetPassword(model.Email, HttpUtility.UrlDecode(model.Token), model.ConfirmPassword);
                if (result.Succeeded)
                {
                    return Json(new
                    {
                        Success = true,
                        Email = model.Email,
                        MsgText = "Success email."
                    });
                }
                else
                {
                    AddErrors(result);
                }
            }
            errorList = ModelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage)
                                 .ToList();
            return Json(new
            {
                Success = false,
                MsgText = "Something went wrong.",
                Errors = errorList
                
            });
        }

        #endregion Reset Password

        // GET: Account
        [HttpGet]
        public ActionResult Index()
        {
            return RedirectToLocal("/Manage/Index");
        }

        // POST: Accounts/CheckEmail
        [HttpPost]
        [AllowAnonymous]
        [ValidateCustomAntiForgeryToken]
        public JsonResult CheckEmail(string email)
        {
            return Json(new { IsEmailValid = _accountService.ValidateEmail(email) });
        }

        // POST: Accounts/LogOff
        [HttpGet]
        public ActionResult LogOff()
        {
            //Add authentication type?
            _authService.AuthenticationManager.SignOut();
            return RedirectToLocal("/Home/Index");
        }

        //GET: Accounts/ResourceTest
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResourceTest()
        {
            var ci = Thread.CurrentThread.CurrentCulture;
            return View();
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        //01

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public const string XsrfKey = "XsrfId";

            public ChallengeResult(string provider, string redirectUrl)
            {
                LoginProvider = provider;
                RedirectUrl = redirectUrl;
            }

            public string LoginProvider { get; set; }
            public string RedirectUrl { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUrl };
                var userId = context.HttpContext.GetOwinContext().Authentication.User.Identity.GetUserId<int>();
                if (userId != 0)
                {
                    properties.Dictionary[XsrfKey] = userId.ToString();
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}