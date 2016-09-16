using System.Web;
using System.Web.Optimization;

namespace Cobra
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            //bundles.Add(new StyleBundle("~/Styles/angular-materials").Include(
            //                "~/App/vendors/angular-material/css/angular-material.min.css",
            //                "~/App/vendors/angular-material/css/docs.css"));


            //bundles.Add(new ScriptBundle("~/bundles/angular-materials").Include(
            //            "~/App/vendors/angular.js",
            //            "~/App/vendors/angular-material/js/angular-animate.min.js",
            //            "~/App/vendors/angular-material/js/angular-route.min.js",
            //            "~/App/vendors/angular-material/js/angular-aria.min.js",
            //            "~/App/vendors/angular-material/js/angular-messages.min.js",
            //            "~/App/vendors/angular-material/js/angular-password.min.js",
            //            "~/App/vendors/angular-material/js/svg-assets-cache.js",
            //            "~/App/vendors/angular-material/js/angular-material.min.js"
            //            ));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
