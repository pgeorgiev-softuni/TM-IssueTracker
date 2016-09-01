using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using TM_IssueTracker.Models;

namespace TM_IssueTracker.Classes
{
    public class AutoLoginAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return base.AuthorizeCore(httpContext);
        }

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {

            // CHANGE TO YOUR USER MANAGER

            /*var principal = new GenericPrincipal(new GenericIdentity("Admin1@foo.com"), new[] { "admin" });

            Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = principal;
            */
            base.OnAuthorization(filterContext);
        }
    }


}