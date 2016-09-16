using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cobra.Identity.IdentityModel;
using Microsoft.Owin;
using Owin;
using Cobra.Identity;
using System.Web.Mvc;

namespace Cobra
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();

            // Configure the db context and user manager to use a single instance per request
            ConfigStartup.InitialiseOwin(app);
            
        }
    }
}