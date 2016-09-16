// Lavesh
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

namespace Cobra.App.Infrastructure.Services
{
    public class UserManagementService : IUserManagementService
    {
        #region data members

        private AuthenticationService _authService;
        private IEmailSendingService _emailService;
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;
        private ApplicationUserStore _userStore;
        #endregion data members

        public UserManagementService(
            ApplicationUserManager userManager,
            ApplicationRoleManager roleManager,
            AuthenticationService authService,
            ApplicationUserStore userStore,
            IEmailSendingService emailService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _authService = authService;
            _emailService = emailService;
            _userStore = userStore;
        }

        public IQueryable<ApplicationUser> GetAllUsers()
        {
            var userList = _userManager.Users;
            return userList;
        }

        public int GetAllUserCount()
        {
            int userCount = _userManager.Users.Count();
            return userCount;
        }

        // Lavesh - 
        // All ToggleUser... functions apply the relevant value held by the passed user object.
        // Changed value is already held by user object.
        // Better way to do this would be to pass just the userID and set the appropriate field to the opposite 
        // of what it currently holds.
        // (less overhead - would be passing an integer only as opposed to the whole user object)
        public bool ToggleUserLockout(ApplicationUser user)
        {
            var result = _userManager.SetLockoutEnabled(user.Id, user.LockoutEnabled);
            return result.Succeeded;
        }
        public bool ToggleEmailConfirmed(ApplicationUser user)
        {
            var userRec = _userManager.FindById(user.Id);
            userRec.EmailConfirmed = user.EmailConfirmed; 
            var result = _userManager.Update(userRec);
            return result.Succeeded;
        }
        public bool TogglePhoneNumberConfirmed(ApplicationUser user)
        {
            var userRec = _userManager.FindById(user.Id);
            userRec.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            var result = _userManager.Update(userRec);
            return result.Succeeded;
        }
        public bool ToggleIsActive(ApplicationUser user)
        {
            var userRec = _userManager.FindById(user.Id);
            userRec.IsActive = user.IsActive;
            var result = _userManager.Update(userRec);
            return result.Succeeded;
        }
        public bool UpdateEmail(ApplicationUser user)
        {
            var result = _userManager.SetEmail(user.Id, user.Email);
            return result.Succeeded;

        }
        public bool UpdatePhone(ApplicationUser user)
        {
            var userRec = _userManager.FindById(user.Id);
            userRec.PhoneNumber = user.PhoneNumber;
            var result = _userManager.Update(userRec);
            return result.Succeeded;

        }
        public bool UpdateFailedAccessCount(ApplicationUser user)
        {
            var userRec = _userManager.FindById(user.Id);
            userRec.AccessFailedCount = user.AccessFailedCount;
            var result = _userManager.Update(userRec);
            return result.Succeeded;
        }
    }
}
