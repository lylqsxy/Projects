using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cobra.Identity.IdentityModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;

namespace Cobra.Identity
{
    public static class ConfigStartup
    {
        public static IAppBuilder InitialiseOwin(IAppBuilder app)
        {
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = new TimeSpan(30, 0, 0, 0),
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser, int>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentityCallback: (manager, user) => user.GenerateUserIdentityAsync(manager), getUserIdCallback: (claim) => int.Parse(claim.GetUserId()))
                }
            });

            //app.Use(async (Context, next) =>
            //{
            //    await next.Invoke();
            //});

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");


            // Author Aakash
            // changed appId and appsecret from my developer account and make social media login public for time being
            app.UseFacebookAuthentication(
               appId:/* "1081339558581169"*/"1081339558581169",
               appSecret:/* "dba5c13222f7b39aa8f118062ac82fce"*/"f584f8e27ef6ca2df565145bb1aefd81"
               );

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "854151064860-70ikhtsf5gf1i6anolhl31a1864cevub.apps.googleusercontent.com",
                ClientSecret = "otFCpy36A3PVd_ymiLx62cUs"
            });
            return app;
        }
    }
}
