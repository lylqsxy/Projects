using System;
using System.Net.Mime;
using System.Web;
using Cobra;
using Cobra.App.Infrastructure.Contracts;
using Cobra.App.Infrastructure.Services;
using Cobra.Identity;
using Cobra.Identity.IdentityModel;
using Cobra.Infrastructure;
using Cobra.Infrastructure.Contracts;
using Cobra.Infrastructure.Data;
using Cobra.Infrastructure.Services;
using Cobra.Model;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Mvc;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
//[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace Cobra
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }



        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                //kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                //kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                //kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
                //kernel.Bind<ApplicationUserManager>()
                //    .ToMethod(ctx => HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>());
                //kernel.Bind<IAccountService>().To<AccountService>();
                //kernel.Bind<IAuthenticationService>().To<AuthenticationService>();
                //kernel.Bind<IAuthenticationManager>().ToMethod(p=>HttpContext.Current.GetOwinContext().Authentication).InRequestScope();
                //kernel.Bind<ILogService>().To<LogService>();
                ////kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
                ////kernel.Bind<IUserStore<ApplicationUser, int>>()
                ////    .ToMethod(ctx => new UserStore<ApplicationUser , ApplicationRole , int , ApplicationUserLogin , ApplicationUserRole , ApplicationUserClaim>(ApplicationDbContext.Create()));
                ////kernel.Bind<UserManager<IdentityUser>>().ToSelf();

                //#region create user 
                //// created by Ty 
                //kernel.Bind<IRepository<Person>>().To<Repository<Person>>();
                //kernel.Bind<IRepository<Profile>>().To<Repository<Profile>>();
                //kernel.Bind<IRepository<Organisation>>().To<Repository<Organisation>>();
                //kernel.Bind<IRepository<Role>>().To<Repository<Role>>();
                //#endregion 

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            DbWorkManager.RegisterFactory(kernel.Get<IUnitOfWork>());
        }
    }
}
