using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace qltb.WebApp.Infrastructure
{
    public class CustomAuthenticationFilter : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            if (filterContext.HttpContext.Session["User"] == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                var url = filterContext.HttpContext.Request.Url;
               
                if (filterContext.HttpContext.Session["DonVi"] == null && !url.AbsolutePath.Contains("/InitUnit"))
                {
                    filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary
                       {
                             { "controller", "InitUnit"},
                             { "action", "InitUnit"}
                       });
                }
            }
        }
        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var url = filterContext.HttpContext.Request.Url;

            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                //Redirecting the user to the Login View of Account Controller
                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary
                {
                     { "controller", "Auth"},
                     { "action", "Login"},
                     { "backurl", url}
                });
            }
        }
    }
}