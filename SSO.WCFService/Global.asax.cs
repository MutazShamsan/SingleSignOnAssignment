using Autofac;
using Autofac.Integration.Wcf;
using System;
using System.IO;

namespace SSO.WCFService
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

            SSO.Common.StaticMembers.ApplicationDirectory = System.Web.HttpContext.Current.Server.MapPath(".");

            // WCF integration docs are at:
            // http://autofac.readthedocs.io/en/latest/integration/wcf.html
            var builder = new ContainerBuilder();

            // Register your service implementations.
            builder.RegisterType<AuthWcfService>().SingleInstance();

            // Register your dependencies.
            SSO.Service.AutofacModule.ModuleRegistration.RegisterApplicationObjects(builder);

            // Set the dependency resolver. This works for both regular
            // WCF services and REST-enabled services.

            AutofacHostFactory.Container = builder.Build();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}