using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SSO.Web.App
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Capture the App location where the logger can find its configuration file
            SSO.Common.StaticMembers.ApplicationDirectory = System.Web.HttpContext.Current.Server.MapPath(".");

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //Call the Mapper here so it is initialize once during the whole lifecycle
            Logic.ServiceObjectMapper.MapWebServiceObjects();
        }
    }
}
