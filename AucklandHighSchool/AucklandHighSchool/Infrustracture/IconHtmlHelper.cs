using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace AucklandHighSchool.Infrustracture
{
    public static class IconHtmlHelper
    {
        public static MvcHtmlString IconLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues, String iconName, object htmlAttributes = null)
        {
            linkText = " " + linkText;
            var linkMarkup = htmlHelper.ActionLink(linkText, actionName, routeValues, htmlAttributes).ToHtmlString() + "&nbsp";
            var iconMarkup = String.Format("<span class=\"{0}\" aria-hidden=\"true\"></span>", iconName);
            return new MvcHtmlString(linkMarkup.Insert(linkMarkup.IndexOf(linkText + @"</a>"), iconMarkup));
        }

        public static MvcHtmlString IconLinkController(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, String iconName, object htmlAttributes = null)
        {
            linkText = " " + linkText;
            var linkMarkup = htmlHelper.ActionLink(linkText, actionName, controllerName, routeValues, htmlAttributes).ToHtmlString() + "&nbsp";
            var iconMarkup = String.Format("<span class=\"{0}\" aria-hidden=\"true\"></span>", iconName);
            return new MvcHtmlString(linkMarkup.Insert(linkMarkup.IndexOf(linkText + @"</a>"), iconMarkup));
        }

        public static MvcHtmlString EditIconLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues = null)
        {
            return htmlHelper.IconLink(linkText, actionName, routeValues, "glyphicon glyphicon-pencil", new { @class = "btn btn-warning" });
        }

        public static MvcHtmlString DetailIconLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues = null)
        {
            return htmlHelper.IconLink(linkText, actionName, routeValues, "glyphicon glyphicon-align-justify", new { @class = "btn btn-info" });
        }

        public static MvcHtmlString AddIconLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues = null)
        {
            return htmlHelper.IconLink(linkText, actionName, routeValues, "glyphicon glyphicon-plus", new { @class = "btn btn-primary" });
        }

        public static MvcHtmlString DeleteIconLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues = null)
        {
            return htmlHelper.IconLink(linkText, actionName, routeValues, "glyphicon glyphicon-trash", new { @class = "btn btn-danger" });
        }

        public static MvcHtmlString CancelIconLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues = null)
        {
            return htmlHelper.IconLink(linkText, actionName, routeValues, "glyphicon glyphicon-remove", new { @class = "btn btn-danger" });
        }

        public static MvcHtmlString IconLinkPost(this HtmlHelper htmlHelper, string buttonText, string actionName, string controllerName, String Name, String Value, String iconName, object htmlAttributes = null)
        {
            buttonText = " " + buttonText;
            var markup = @"<div style=""display: inline-block"">";
            markup += String.Format(@"<form action=""/{0}/{1}"" method=""post"">", controllerName, actionName);
            markup += String.Format(@"<input id=""{0}"" name=""{1}"" type=""hidden"" value=""{2}"" />", Name, Name, Value);
            markup += String.Format(@"<button type =""submit"" class=""{0}"" >", htmlAttributes);
            markup += String.Format(@"<span class=""{0}"" aria -hidden=""true"">", iconName);
            markup += String.Format(@"</span>{0}</button></form></div>&nbsp", buttonText);
            return new MvcHtmlString(markup);
        }

        public static MvcHtmlString DeleteIconLinkPost(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, String Name, String Value)
        {
            return htmlHelper.IconLinkPost(linkText, actionName, controllerName, Name, Value, "glyphicon glyphicon-trash", "btn btn-danger");
        }

        public static MvcHtmlString IconButtonSubmit(this HtmlHelper htmlHelper, string buttonText, String iconName, object htmlAttributes = null)
        {
            buttonText = " " + buttonText;
            var markup = String.Format(@"<button type =""submit"" class=""{0}"" >", htmlAttributes);
            markup += String.Format(@"<span class=""{0}"" aria -hidden=""true"">", iconName);
            markup += String.Format(@"</span>{0}</button>&nbsp", buttonText);
            return new MvcHtmlString(markup);
        }

        public static MvcHtmlString OKIconButtonSubmit(this HtmlHelper htmlHelper, string buttonText)
        {
            return htmlHelper.IconButtonSubmit(buttonText, "glyphicon glyphicon-ok", "btn btn-success");
        }

        public static MvcHtmlString BackIconLInk(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues = null)
        {
            return htmlHelper.IconLinkController(linkText, actionName, controllerName, routeValues, "glyphicon glyphicon-chevron-left", new { @class = "btn btn-primary" });
        }      
    }
}