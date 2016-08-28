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
            return htmlHelper.IconLink(linkText, actionName, routeValues, "glyphicon glyphicon-list", new { @class = "btn btn-info" });
        }

        public static MvcHtmlString AddIconLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues = null)
        {
            return htmlHelper.IconLink(linkText, actionName, routeValues, "glyphicon glyphicon-plus", new { @class = "btn btn-primary" });
        }

        public static MvcHtmlString DeleteIconLink(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues = null)
        {
            return htmlHelper.IconLink(linkText, actionName, routeValues, "glyphicon glyphicon-trash", new { @class = "btn btn-danger" });
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

        public static MvcHtmlString IconUrlLink(this HtmlHelper htmlHelper, string linkText, string url, String iconName, object htmlAttributes = null)
        {
            var markup = String.Format(@"<a href =""{0}"" class=""{1}"" >", url, htmlAttributes);
            markup += String.Format(@"<span class=""{0}"" aria -hidden=""true"">", iconName);
            markup += String.Format(@"</span>{0}</a>", linkText);
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

        public static MvcHtmlString AddIconLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues = null)
        {
            return htmlHelper.IconLinkController(linkText, actionName, controllerName, routeValues, "glyphicon glyphicon-plus", new { @class = "btn btn-primary" });
        }

        public static MvcHtmlString EditIconLinkSmall(this HtmlHelper htmlHelper, string linkText, string actionName, object routeValues = null)
        {
            return htmlHelper.IconLink(linkText, actionName, routeValues, "glyphicon glyphicon-pencil", new { @class = "btn btn-default btn-sm" });
        }

        public static MvcHtmlString DeleteIconLinkPostSmall(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, String Name, String Value)
        {
            return htmlHelper.IconLinkPost(linkText, actionName, controllerName, Name, Value, "glyphicon glyphicon-trash", "btn btn-danger btn-sm");
        }

        public static MvcHtmlString AddIconButtonSubmit(this HtmlHelper htmlHelper, string buttonText)
        {
            return htmlHelper.IconButtonSubmit(buttonText, "glyphicon glyphicon-plus", "btn btn-primary");
        }

        public static MvcHtmlString LinkPost(this HtmlHelper htmlHelper, string buttonText, string actionName, string controllerName, String Name, String Value, object htmlAttributes = null)
        {
            buttonText = " " + buttonText;
            var markup = @"<div style=""display: inline-block"">";
            markup += String.Format(@"<form action=""/{0}/{1}"" method=""post"">", controllerName, actionName);
            markup += String.Format(@"<input id=""{0}"" name=""{1}"" type=""hidden"" value=""{2}"" />", Name, Name, Value);
            markup += String.Format(@"<button type =""submit"" class=""{0}"" >", htmlAttributes);
            markup += String.Format(@"{0}</button></form></div>&nbsp", buttonText);
            return new MvcHtmlString(markup);
        }

        public static MvcHtmlString DeleteLinkPost(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, String Name, String Value)
        {
            return htmlHelper.LinkPost(linkText, actionName, controllerName, Name, Value, "btn btn-danger");
        }

        public static MvcHtmlString ButtonSubmit(this HtmlHelper htmlHelper, string buttonText, object htmlAttributes = null)
        {
            buttonText = " " + buttonText;
            var markup = String.Format(@"<button type =""submit"" class=""{0}"" >", htmlAttributes);
            markup += String.Format(@"{0}</button>&nbsp", buttonText);
            return new MvcHtmlString(markup);
        }

        public static MvcHtmlString ButtonGoBack(this HtmlHelper htmlHelper, string buttonText, object htmlAttributes = null)
        {
            buttonText = " " + buttonText;
            var markup = String.Format(@"<button type =""button"" class=""{0}"" onclick=window.history.back()>", htmlAttributes);
            markup += String.Format(@"{0}</button>&nbsp", buttonText);
            return new MvcHtmlString(markup);
        }

        public static MvcHtmlString IconButtonGoBack(this HtmlHelper htmlHelper, string buttonText, String iconName, object htmlAttributes = null)
        {
            buttonText = " " + buttonText;
            var markup = String.Format(@"<button type =""button"" class=""{0}"" onclick=window.history.back()>", htmlAttributes);
            markup += String.Format(@"<span class=""{0}"" aria -hidden=""true"">", iconName);
            markup += String.Format(@"</span>{0}</button>&nbsp", buttonText);
            return new MvcHtmlString(markup);
        }

        
        public static MvcHtmlString IconGoBack(this HtmlHelper htmlHelper, string buttonText)
        {
            return htmlHelper.IconButtonGoBack(buttonText, "glyphicon glyphicon-chevron-left", new { @class = "btn btn-primary" });
        }

        public static MvcHtmlString BackToListIconLinkController(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues = null)
        {
            return htmlHelper.IconLinkController(linkText, actionName, controllerName, routeValues, "glyphicon glyphicon-share-alt", new { @class = "btn btn-default" });
        }

        public static MvcHtmlString CancelIconUrlLink(this HtmlHelper htmlHelper, string linkText, string url)
        {
            return htmlHelper.IconUrlLink(linkText, url, "glyphicon glyphicon-remove", new { @class = "btn btn-danger" });
        }
    }
}