using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cobra.App.Infrastructure.Contracts;
using Microsoft.AspNet.Identity;

namespace Cobra.App.Infrastructure.Services
{
    public class SmsSendingService : ISmsSendingService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // TODO: implement this method if needed
            throw new NotImplementedException();
        }
    }
}
