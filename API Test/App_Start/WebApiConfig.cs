using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API_Test
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web-API-Konfiguration und -Dienste

            // Web-API-Routen
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{version}",
                defaults: new { version = RouteParameter.Optional }
            );
        }
    }
}
