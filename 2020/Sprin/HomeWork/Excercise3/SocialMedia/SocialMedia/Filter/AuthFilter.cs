using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SocialMedia.Data;

namespace SocialMedia.Filters
{
    public class AuthFilterAttribute : Attribute, IAuthorizationFilter
    {
        private UsersContext _usersContext = null;
        public AuthFilterAttribute()
        {
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _usersContext = (UsersContext)context.HttpContext.RequestServices.GetService(typeof(UsersContext));

            var token = context.HttpContext.Request.Cookies["token"];
            var id = context.HttpContext.Request.Cookies["id"];

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new RedirectResult("/Account/Login");
            }
            if (!IsAuth(double.Parse(token), int.Parse(id), context))
                context.Result = new BadRequestResult();
        }

        private bool IsAuth(double token, int id, AuthorizationFilterContext context)
        {
            var user = _usersContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                context.Result = new NotFoundResult();

            return token == MyHashCode.GetHash(user.Email + user.Password);
        }
    }
}