using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TMS
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            
               routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "FormIndex",
            url: "form/{action}",
                defaults: new { controller = "Form", action = "Index" }
            );
            
            routes.MapRoute(
                name: "Ticket",
            url: "Ticket/{action}",
                defaults: new { controller = "Ticket", action = "Index" }
            );
            routes.MapRoute(
                name: "Roles",
            url: "Roles/{action}",
                defaults: new { controller = "Roles", action = "Index" }
            );

            routes.MapRoute(
                name: "CommonLookup",
            url: "CommonLookup/{action}",
                defaults: new { controller = "CommonLookup", action = "Index" }
            );

            routes.MapRoute(
                name: "Users",
            url: "Users/{action}",
                defaults: new { controller = "Users", action = "Index" }
            );



            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",

                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
                
                

            );


        }
    }
}
