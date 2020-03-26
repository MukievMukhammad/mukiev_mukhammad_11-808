using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SocialMedia.Filters
{
    public class AuthFilter : Attribute, IAuthorizationFilter
    {
        public AuthFilter()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Cookies["token"];
            if (token == null || token == "")
            {
                context.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}