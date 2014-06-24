using System.Web.Mvc;
using System.Web.Routing;
using Umbraco.Core;

namespace U7StandardMembership.Code
{
    public class UmbracoStartup : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            //Register custom MVC route for user profile
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                "memberProfileRoute",
                "user/{profileURLtoCheck}",
                new
                {
                   controller = "ViewProfile",
                   action = "Index"
                });
        }
    }
}