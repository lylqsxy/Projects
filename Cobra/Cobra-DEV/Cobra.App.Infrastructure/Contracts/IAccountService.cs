using System.Threading.Tasks;
using Cobra.Identity.IdentityModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections;
using System.Collections.Generic;
using Cobra.Model;
using System.Linq;
using Cobra.App.Infrastructure.Services;

namespace Cobra.App.Infrastructure.Contracts
{
    public interface IAccountService
    {
        bool ValidateEmail(string email);
        Task<IdentityResult> CreateUserLogin(string email, string password);
        Task<IdentityResult> CreateExternalLogin(string email, ExternalLoginInfo loginInfo);
        Task<IdentityResult> ConfirmEmail(string token, string email);
        int GetCurrentUserId();
        string GetCurrentUserName();
        ApplicationUser GetCurrentUser();
        IQueryable<ApplicationUser> GetAllUsers();
        Task<IdentityResult> ForgetPassword(string email);
        Task<IdentityResult> ResetPassword(string email, string token, string newPassword);
    }
}
