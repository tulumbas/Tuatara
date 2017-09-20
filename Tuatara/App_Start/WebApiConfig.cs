using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Tuatara
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // set unity resolver
            //config.DependencyResolver = ...

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { action = "get", id = RouteParameter.Optional }
            );

            var jsonFormater = config.Formatters.JsonFormatter;
            // make json default format for output
            jsonFormater.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            // for nice json
            jsonFormater.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            jsonFormater.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
            // to resolve serialization of Parent => self reference
            jsonFormater.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }
    }
}
