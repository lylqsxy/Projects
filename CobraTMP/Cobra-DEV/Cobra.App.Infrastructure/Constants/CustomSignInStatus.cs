using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cobra.App.Infrastructure.Constants
{
    public enum CustomSignInStatus
    {
        //
        // Summary:
        //     Sign in was successful
        Success = 0,
        //
        // Summary:
        //     Email confirmation required
        RequireEmailConfirm = 1,
        //
        // Summary:
        //     User is locked out
        LockedOut = 2,
        //
        // Summary:
        //     Sign in requires addition verification (i.e. two factor)
        RequiresVerification = 3,
        //
        // Summary:
        //     Sign in failed
        Failure = 4
    }
}
