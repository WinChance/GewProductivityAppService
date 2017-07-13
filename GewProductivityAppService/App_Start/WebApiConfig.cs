using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace GewProductivityAppService
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            //默认返回 json  
            GlobalConfiguration.Configuration.Formatters
                .JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("datatype", "json", "application/json"));
            //返回格式选择  
            GlobalConfiguration.Configuration.Formatters
                .XmlFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("datatype", "xml", "application/xml"));
            //json 序列化设置  
            //GlobalConfiguration.Configuration.Formatters
            //    .JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings()
            //    {
            //        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore //设置忽略值为 null 的属性  
            //    };  
        }
    }
}
