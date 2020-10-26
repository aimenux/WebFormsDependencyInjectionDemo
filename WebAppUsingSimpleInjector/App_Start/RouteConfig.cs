using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace WebAppUsingSimpleInjector
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings
            {
                AutoRedirectMode = RedirectMode.Permanent
            };

            routes.EnableFriendlyUrls(settings);
            routes.MapPageRoute("default", "", "~/Pages/HomePage.aspx");
        }
    }
}
