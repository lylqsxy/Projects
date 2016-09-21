using System.Threading.Tasks;
using Cobra.App.Infrastructure.Constants;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Cobra.App.Infrastructure.Contracts
{
    public interface IAuthenticationService
    {
        Task<ExternalLoginInfo>  CustomGetExternalLoginInfo();
        void ExternalLoginChallenge(string provider, string returnUri, int userId, IOwinContext currentContext);
        bool IsAuthenticated();
        Task<CustomSignInStatus> CustomPasswordSignIn(string email, string password, bool rememberMe = false, bool twoFactorRememberBrowser = false);
        //Use "IAuthenticationService" Extend "ApplicationSignInManager" when needed
    }
}
