using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MOM_PROJECT.Filters
{
    /// <summary>
    /// Global action filter that checks if the user is logged in (session has "UserID").
    /// If the user is NOT logged in → redirect to Account/Login.
    /// Controllers or actions marked with [AllowAnonymous] are skipped automatically.
    /// </summary>
    public class CheckAccess : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // ── Skip check if the action or controller has [AllowAnonymous] ──
            if (context.ActionDescriptor.EndpointMetadata
                    .Any(em => em.GetType() == typeof(Microsoft.AspNetCore.Authorization.AllowAnonymousAttribute)))
            {
                base.OnActionExecuting(context);
                return;
            }

            // ── If user is NOT logged in → redirect to Login page ──
            var userId = context.HttpContext.Session.GetString("UserID");
            if (string.IsNullOrEmpty(userId))
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
                return;
            }

            base.OnActionExecuting(context);
        }
    }
}
