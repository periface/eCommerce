using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PymeTamFinal
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Inicio", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Tienda_Detalle",
                url: "{controller}/{action}/{id}/{slug}",
                defaults: new { controller = "Tienda" ,action = "DetalleProducto", id = UrlParameter.Optional, slug = UrlParameter.Optional }
            );
        }
    }
}
