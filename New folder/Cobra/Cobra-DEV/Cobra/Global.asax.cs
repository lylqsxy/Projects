using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cobra;
using Cobra.Configuration;
using Cobra.Filters;
using Cobra.Infrastructure.Data;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Cobra.App.Infrastructure.Services;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(MvcApplication), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(MvcApplication), "Stop")]
namespace Cobra
{
    public class MvcApplication : NinjectHttpApplication
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Start()
        {
            //ControllerBuilder.Current.SetControllerFactory(new DefaultControllerFactory(new CultureAwareControllerActivator()));

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
                 filters.Add(new HandleErrorAttribute());
        }
        protected void Application_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            //Trace.WriteLine("Application_PreRequestHandlerExecute");
            DbWorkManager.Start();
        }
        protected void Application_PostRequestHandlerExecute(object sender, EventArgs e)
        {
            //Trace.WriteLine("Application_PostRequestHandlerExecute");
            DbWorkManager.End();
        }
        public void Application_End()
        {
            
        }

        protected override IKernel CreateKernel()
        {
            
            var kernel = new StandardKernel(new AppInfrastructureNinjectModule());
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
            RegisterServices(kernel);
            var resolver = new NinjectMvcDependencyResolver(kernel);
            DependencyResolver.SetResolver(resolver);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            DbWorkManager.RegisterFactory(kernel.Get<IUnitOfWork>());
        }

        protected void OnApplicationStarted()
        {

        }

        protected void OnApplicationStopped()
        {

        }
    }
}
