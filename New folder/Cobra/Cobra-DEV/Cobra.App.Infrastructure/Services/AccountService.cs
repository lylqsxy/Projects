using System;
using System.Threading.Tasks;
using System.Web;
using Cobra.App.Infrastructure.Contracts;
using Cobra.Identity;
using Cobra.Identity.IdentityModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Cobra.App.Infrastructure.Helplers;
using System.Collections.Specialized;
using System.Linq;
using Cobra.Infrastructure.Services;


namespace Cobra.App.Infrastructure.Services
{
    public class AccountService : IAccountService
    {

        #region Data Members

        private AuthenticationService _authService;
        private IEmailSendingService _emailService;
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;
        private LogService log = new LogService();

        #endregion Data Members

        public AccountService(
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager,
            AuthenticationService authService,
            IEmailSendingService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
            _emailService = emailService;
        }

        public async Task<IdentityResult> ConfirmEmail(string token, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new IdentityResult("Invalid Token");
            }
            if (user.IsActive && !user.EmailConfirmed)
            {
                if (!DateTimeHelper.IsExpired(user.EmailConfirmationTokenExpiryDate))
                {
                    var result = await _userManager.ConfirmEmailAsync(user.Id, token);
                    if (result.Succeeded)
                    {
                        user.EmailConfirmationTokenExpiryDate = null;
                        await _userManager.UpdateAsync(user);
                        return result;
                    }
                    return result;
                }
                return new IdentityResult("Invalid Token");
            }
            return new IdentityResult("Invalid Token");
        }

        public async Task<IdentityResult> CreateExternalLogin(string email, ExternalLoginInfo loginInfo)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                IsActive = true,
            };
            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                result = await _userManager.AddLoginAsync(user.Id, loginInfo.Login);
                await _authService.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                return result;
            }
            return result;
        }

        public async Task<IdentityResult> CreateUserLogin(string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                IsActive = true,
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var token = _userManager.GenerateEmailConfirmationToken(user.Id);
                user.EmailConfirmationToken = token;
                user.EmailConfirmationTokenExpiryDate = DateTime.Now.AddDays(2).ToUniversalTime();
                await _userManager.UpdateAsync(user);
                var emailResult = await SendConfirmationEmail(user);
            }
            return result;
        }

        public async Task<IdentityResult> ForgetPassword(string email)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
            {
                return new IdentityResult("Inv alid Email");
            }
            string token = await _userManager.GeneratePasswordResetTokenAsync(user.Id);
            user.ResetPasswordToken = token;
            user.ResetPasswordTokenExpiryDate = DateTime.Now.AddDays(2).ToUniversalTime();
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                var nvc = new NameValueCollection();
                nvc.Set("token", token);
                nvc.Set("email", email);
                var request = HttpContext.Current.Request;
                var url = new UriBuilder();
                url.Scheme = request.Url.Scheme;
                url.Host = request.Url.Host;
                url.Path = "Account/ResetPassword";
                url.Query = StringHelpler.ToQueryString(nvc);

                string subject = "Reset Password";
                string body =
                      "Please reset your password by clicking <a target='_blank' href=" + url.ToString() + "> Click Here </a>";

                // Send email
                IdentityMessage Msg = new IdentityMessage
                {
                    Destination = user.Email,
                    Body = body,
                    Subject = subject,
                };
                try
                {
                    await _emailService.SendAsync(Msg);
                }
                catch (Exception ex)
                {
                    if (ex != null)
                    {
                        log.Error(ex.Message, ex);
                        new IdentityResult("Email Server Error");
                    }
                }
                return result;
            }
            return result;
            
        }

        //Helen
        public IQueryable<ApplicationUser> GetAllUsers()
        {
            var userList = _userManager.Users;
            return userList;
        }

        public ApplicationUser GetCurrentUser()
        {
            return _userManager.FindById(GetCurrentUserId());
        }

        public int GetCurrentUserId()
        {
            return HttpContext.Current.User.Identity.GetUserId<int>(); ;
        }

        public string GetCurrentUserName()
        {
            return HttpContext.Current.User.Identity.Name;
        }

        public async Task<IdentityResult> ResetPassword(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(email);
            if (user == null)
            {
                return new IdentityResult("Invalid Email");
            }
            var expireDate = user.ResetPasswordTokenExpiryDate.HasValue ? user.ResetPasswordTokenExpiryDate.Value : DateTime.UtcNow.AddDays(-1);
            if (!DateTimeHelper.IsExpired(user.ResetPasswordTokenExpiryDate))
            {
                var result = await _userManager.ResetPasswordAsync(user.Id, token, newPassword);
                user.ResetPasswordToken = "";
                user.ResetPasswordTokenExpiryDate = null;
                await _userManager.UpdateAsync(user);
                await _userManager.UpdateSecurityStampAsync(user.Id);
                return result;
            }
            else
            {
                return new IdentityResult("Invalid Token");
            }

        }

        public bool ValidateEmail(string email)
        {
            var result = _userManager.FindByName(email);
            if (result != null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private async Task<bool> SendConfirmationEmail(ApplicationUser user)
        {
            //TODO:
            //create email template 
            var request = HttpContext.Current.Request;
            var nvc = new NameValueCollection();
            nvc.Set("token", user.EmailConfirmationToken);
            nvc.Set("email", user.Email);
            var url = new UriBuilder();
            url.Scheme = request.Url.Scheme;
            url.Host = request.Url.Host;
            url.Path = "Account/ConfirmEmail";
            url.Query = StringHelpler.ToQueryString(nvc);
            string subject = "Welcome! Please Activate Your Account";

            string body =
                  "You(or someone else) just create an account for this email,<br />"
                + "Please <a target='_blank' href=" + url.ToString() + "> Click Here </a>"
                + "to activate your account.<br />"
                + "This link will expire in 48 hours.";

            // Send email
            IdentityMessage Msg = new IdentityMessage
            {
                Destination = user.Email,
                Body = body,
                Subject = subject,
            };
            try
            {
                await _emailService.SendAsync(Msg);
            }
            catch (Exception ex)
            {
                if (ex != null)
                {
                    log.Error(ex.Message, ex);
                    return false;
                }
            }
            return true;
        }
    }
}