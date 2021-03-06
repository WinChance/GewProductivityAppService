﻿using System;
using System.Globalization;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.Cors;
using GewProductivityAppService.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace GewProductivityAppService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //跨域配置
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //默认返回 json  
            GlobalConfiguration.Configuration.Formatters
                .JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("datatype", "json", "application/json"));


            config.Filters.Add(new WebApiExceptionFilterAttribute());


        }
    }
}
