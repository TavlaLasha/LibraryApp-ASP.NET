using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LibraryClientApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Product",
                url: "{controller}/ChangeArchiveStatus/{isbn}",
                defaults: new { controller = "Library", action = "ChangeArchiveStatus", isbn = "" }
            );

            routes.MapRoute(
                name: "Library",
                url: "{controller}/{action}/{pn}",
                defaults: new { controller = "Library", action = "Index", pn=""}
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
