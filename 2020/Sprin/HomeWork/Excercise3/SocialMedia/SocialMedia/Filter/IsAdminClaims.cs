using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialMedia.Data;

namespace SocialMedia.Filter
{
    public class IsAdminClaims : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userId = context.HttpContext.Request.Cookies["id"];
            if (!string.IsNullOrEmpty(userId))
            {
                var userContext = (UsersContext)context.HttpContext.RequestServices.GetService(typeof(UsersContext));
                var user = userContext.Users.FirstOrDefault(u => u.Id == int.Parse(userId));
                if(user == null || !user.IsAdmin)
                {
                    context.HttpContext.Response.WriteAsync("402 Forbidden!! Permission denied!");
                    context.Result = new ForbidResult();
                }
                base.OnActionExecuting(context);
            }
        }
    }
}
