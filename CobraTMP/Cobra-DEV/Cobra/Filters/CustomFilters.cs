using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Cobra.Filters
{
    //Use with helper: Html.AntiForgeryToken()
    //In AngularJS, to capture XSRF token value, use:
    //  angular.element('input[name="__RequestVerificationToken"]').attr('value')
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ValidateCustomAntiForgeryTokenAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            var httpContext = filterContext.HttpContext;
            var formToken = httpContext.Request.Headers["X-XSRF-Token"];
            var cookie = httpContext.Request.Cookies[AntiForgeryConfig.CookieName];
            try
            {
                AntiForgery.Validate(cookie != null ? cookie.Value : null, formToken);
            }
            catch (HttpAntiForgeryException)
            {
                throw new HttpAntiForgeryException("Anti forgery token cookie not found.");
            }
        }
    }
}