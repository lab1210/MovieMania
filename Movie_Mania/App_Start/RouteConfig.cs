using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Movie_Mania
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "MovieDetails",
                url: "MovieDetails/{slug}",
                defaults: new { controller = "Home", action = "MovieDetails", slug = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "SeriesDetails",
                url: "SeriesDetails/{slug}",
                defaults: new { controller = "Home", action = "SeriesDetails", slug = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "TvSeries", id = UrlParameter.Optional }
            );
        }
    }
}
