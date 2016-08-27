using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TM_IssueTracker
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Comments",
                url: "Projects/{pid}/Issues/{sid}/Comments/{action}/{id}",
                defaults: new { controller = "Comments", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Issues",
                url: "Projects/{pid}/Issues/{action}/{id}",
                defaults: new { controller = "Issues", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Projects",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Projects", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
