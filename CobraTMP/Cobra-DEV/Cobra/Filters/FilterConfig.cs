using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;

namespace Cobra.Filters
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters, IKernel kernel)
        {
            //filters.Add(new LHDErrorAttribute());
            filters.Add(new AuthorizeAttribute());

            //Ensure No cache is global.
            // TWH: Don't we want some actions such as ProfileImage to be cached, unless client cache-busted?
            //filters.Add(new NoCacheAttribute());

            //Add additional headers for security
            //filters.Add(new SecurityHtmlHeadersFilterAttribute());

            ////add the global filter only if configured to do so
            ////may need to be turned off for automated testing of controllers
            //if (Convert.ToBoolean(ConfigurationManager.AppSettings["EnableAntiforgeryTokens"]))
            //{
            //    //Add additional headers for security
            //    filters.Add(new GlobalAntiForgeryTokenAttribute());
            //}
        }
    }
}