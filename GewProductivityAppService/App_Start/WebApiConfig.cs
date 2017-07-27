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
            //默认返回 json  
            GlobalConfiguration.Configuration.Formatters
                .JsonFormatter.MediaTypeMappings.Add(
                new QueryStringMapping("datatype", "json", "application/json"));
            //返回格式选择  
            //GlobalConfiguration.Configuration.Formatters
            //    .XmlFormatter.MediaTypeMappings.Add(
            //    new QueryStringMapping("datatype", "xml", "application/xml"));
            //InitAPI.Init(config);
        }
    }
}
