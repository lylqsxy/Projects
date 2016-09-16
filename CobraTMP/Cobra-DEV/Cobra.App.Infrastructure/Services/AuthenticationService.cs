using Microsoft.Owin.Host.SystemWeb;
using System.Web;
using System.Threading.Tasks;
using Cobra.App.Infrastructure.Contracts;
using Cobra.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using System.Web.Mvc;
using System;
using Cobra.App.Infrastructure.Constants;
using Cobra.App.Infrastructure.Helplers;
using System.Configuration;

namespace Cobra.App.Infrastructure.Services
{
    public class AuthenticationService : ApplicationSignInManager, IAuthenticationService
    {
        private ApplicationUserManager _userManager;
        private IAuthenticationManager _authenticationManager;
        private const string XsrfKey = "XsrfId";

        public AuthenticationService(
            ApplicationUserManager userManager,
            IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
        }

        public async Task<ExternalLoginInfo> CustomGetExternalLoginInfo()
        {
            return await AuthenticationManagerExtensions.GetExternalLoginInfoAsync(_authenticationManager);
        }

        public void ExternalLoginChallenge(string provider, string returnUri, int userId, IOwinContext currentContext)
        {
            var properties = new AuthenticationProperties { RedirectUri = returnUri };
            if (userId != 0)
            {
                properties.Dictionary[XsrfKey] = userId.ToString();
            }
            _authenticationManager.Challenge(properties, provider);
        }

        public bool IsAuthenticated()
        {
            return _authenticationManager.User.Identity.IsAuthenticated;
        }

        public async Task<CustomSignInStatus> CustomPasswordSignIn(string email, string password, bool rememberMe, bool twoFactorRememberBrowser = false)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && user.IsActive)
            {
                var validCredential = await _userManager.FindAsync(email, password);
                if (!user.EmailConfirmed)
                {
                    return CustomSignInStatus.RequireEmailConfirm;
                }
                else if (await _userManager.IsLockedOutAsync(user.Id))
                {
                    return CustomSignInStatus.LockedOut;
                }
                else if (validCredential == null)
                {
                    await _userManager.AccessFailedAsync(user.Id);
                    return CustomSignInStatus.Failure;
                }
                else
                {
                    //TODO:
                    //Implement 2-factor verification in future
                    await SignInAsync(validCredential, rememberMe, twoFactorRememberBrowser);
                    return CustomSignInStatus.Success;
                }
            }
            return CustomSignInStatus.Failure;
        }
        //Use "IAuthenticationService" Extend "ApplicationSignInManager" when needed
    }
}
