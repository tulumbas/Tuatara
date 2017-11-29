using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Tuatara
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                name: "Default",
                url: "{p}",
                defaults: new { controller = "Root", action = "Index", p = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default2",
                url: "{p}/{p2}",
                defaults: new { controller = "Root", action = "Index", p2 = UrlParameter.Optional }
            );
        }
    }
}
