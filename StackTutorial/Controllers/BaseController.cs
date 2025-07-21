using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;

namespace StackTutorial.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly HashSet<string> NoBreadcrumbActions = new()
        {
            // Add any actions that should never show breadcrumbs
        };

        protected void HideBreadcrumbs() => ViewData["HideBreadcrumbs"] = true;
        protected void ShowBreadcrumbs() => ViewData["HideBreadcrumbs"] = false;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionName = context.ActionDescriptor.RouteValues["action"];
            if (NoBreadcrumbActions.Contains(actionName))
            {
                HideBreadcrumbs();
            }

            base.OnActionExecuting(context);
        }
    }
}