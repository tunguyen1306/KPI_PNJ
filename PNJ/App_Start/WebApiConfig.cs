using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PNJ
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Routes.MapHttpRoute(
                name: "two",
                routeTemplate: "api/{controller}/{action}/{type}/{condition}",
                defaults: new { type = RouteParameter.Optional }
            );
        }
    }
}
