 // Lavesh
using System.Threading.Tasks;
using Cobra.Identity.IdentityModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Collections;
using System.Collections.Generic;
using Cobra.Model;
using System.Linq;

namespace Cobra.App.Infrastructure.Contracts
{
    public interface IUserManagementService
    {
        IQueryable<ApplicationUser> GetAllUsers();
        int GetAllUserCount();
        bool ToggleUserLockout(ApplicationUser user);
        bool ToggleEmailConfirmed(ApplicationUser user);
        bool TogglePhoneNumberConfirmed(ApplicationUser user);
        bool ToggleIsActive(ApplicationUser user);
        bool UpdateEmail(ApplicationUser user);
        bool UpdatePhone(ApplicationUser user);
        bool UpdateFailedAccessCount(ApplicationUser user);
  
    }
}
