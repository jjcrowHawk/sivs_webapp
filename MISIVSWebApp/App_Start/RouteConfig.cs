using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MISIVSWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Accounts", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Features",
                url: "{controller}/{action}/{sector}/{feature}",
                defaults: new { controller = "Home", action = "Index", feature = UrlParameter.Optional }
            );

        }
    }
}
