using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TopluMailProjesi.Filter
{
    public class UserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string? userId = context.HttpContext.Session.GetString("id");

            
            if (String.IsNullOrEmpty(userId))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"action", "Index" },
                    {"controller","Account" }
                });
            }
            base.OnActionExecuting(context);
        }
    }
}
