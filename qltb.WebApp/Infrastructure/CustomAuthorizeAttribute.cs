using qltb.WebApp.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace qltb.WebApp.Infrastructure
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string[] allowedroles;
        public CustomAuthorizeAttribute(params string[] roles)
        {
            this.allowedroles = roles;
        }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;
            if (httpContext.Session["User"] != null)
            {
                var nguoiDung = (httpContext.Session["User"] as qltb.Data.NguoiDung);

                var roles = Business.StaticData.getRoles(nguoiDung.NguoiDungId.ToString());
                roles.Add("ALL");
                if (roles.Count>0)
                {
                    foreach (var role in allowedroles)
                    {
                        if (roles.Contains(role)) return true;
                    }
                }
            }
            else
            {
                return false;
            }
            return authorize;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session["User"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                 new RouteValueDictionary
                 {
                        { "controller", "Auth" },
                        { "action", "Login" }
                 });
            }
            else
            {
                var url = filterContext.HttpContext.Request.Url;
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                 {
                        { "controller", "Auth" },
                        { "action", "UnAuthorized" },
                        { "backurl", url}
                 });
            }

        }
    }
}