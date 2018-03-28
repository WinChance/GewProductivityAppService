using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using GewProductivityAppService.Service.Quartz;
using Newtonsoft.Json.Converters;

namespace GewProductivityAppService
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Filters.Add(new WebApiExceptionFilterAttribute());

            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(Server.MapPath("Log4net.config")));
            // 自定义事件注册
            
            QiangDanPushJobScheduler.Start();
            Yd2PrShouSongZhouPushJobScheduler.Start();

        }

        /// <summary>
        /// IIS应用池回收造成Application_Start中定时执行程序停止的问题的解决方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_End(object sender, EventArgs e)
        {
            // 在应用程序关闭时运行的代码 
            //解决应用池回收问题 
            System.Threading.Thread.Sleep(5000);
            //string strUrl = "192.168.22.125:8889";
            // "192.168.7.38/GEWProductivityAppSer"
            string strUrl = "192.168.7.38/GEWProductivityAppSer";
            System.Net.HttpWebRequest _HttpWebRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(strUrl);
            System.Net.HttpWebResponse _HttpWebResponse = (System.Net.HttpWebResponse)_HttpWebRequest.GetResponse();
            System.IO.Stream _Stream = _HttpWebResponse.GetResponseStream();//得到回写的字节流 
        } 
    }
}
