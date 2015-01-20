using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SuiviActivite.Web.Custom.HtmlHelpers
{
    public static class HtmlHelperExtensionMethods
    {
        public static MvcHtmlString ActionMenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName)
        {
            return ActionMenuLink(htmlHelper, linkText, actionName, controllerName, false);
        }

        public static MvcHtmlString ActionMenuLink(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, bool ignoreAction)
        {
            string currentAction = htmlHelper.ViewContext.RouteData.GetRequiredString("action");
            string currentController = htmlHelper.ViewContext.RouteData.GetRequiredString("controller");

            if (currentController.ToUpper() == controllerName.ToUpper() && (ignoreAction || currentAction.ToUpper() == actionName.ToUpper()))
            {
                return htmlHelper.ActionLink(linkText, actionName, controllerName, new { @class = "activeMenuItem" });
            }

            return htmlHelper.ActionLink(linkText, actionName, controllerName);
        }
    }
}