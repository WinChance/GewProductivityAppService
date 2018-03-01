using System.Web.Mvc;

namespace GewProductivityAppService.Areas.QiWebApp
{
    public class QiWebAppAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "QiWebApp";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "QiWebApp_default",
                "QiWebApp/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}