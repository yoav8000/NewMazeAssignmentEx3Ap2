using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace NewMazeAssignmentEx3Ap2
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();


            config.Routes.MapHttpRoute(
                name: "ContactApi",
               routeTemplate: "api/{controller}/{mazeName}/{rows}/{cols}"
          );

            config.Routes.MapHttpRoute(
               name: "ContactApi1",
              routeTemplate: "api/{controller}/{mazeName}/{searchAlgorithm}"
         );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
