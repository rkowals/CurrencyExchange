﻿using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CurrencyExchange
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            var settings = config.Formatters.JsonFormatter.SerializerSettings;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            settings.Formatting = Newtonsoft.Json.Formatting.Indented;

            // Web API routes

            config.MapHttpAttributeRoutes();

                 config.Routes.MapHttpRoute(
                   name: "DefaultApi",
                   routeTemplate: "{controller}/{id}",
                   defaults: new { id = RouteParameter.Optional }
                );
        }
    }
}
