using Autofac;
using Autofac.Integration.Mvc;
using SSO.WebApp1.ActionFilters;
using SSO.WebApp1.IoC;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SSO.WebApp1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

           
        }
    }
}
