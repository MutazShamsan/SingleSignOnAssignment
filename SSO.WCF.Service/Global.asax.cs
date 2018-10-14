using Autofac;
using Autofac.Integration.Wcf;
using System;
using System.ServiceModel.Activation;
using System.Web.Routing;

namespace SSO.WCF.Service
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            // WCF integration docs are at:
            // http://autofac.readthedocs.io/en/latest/integration/wcf.html
            var builder = new ContainerBuilder();

            // Register your service implementations.
            builder.RegisterType<SignInService>();

            // Register your dependencies.
            SSO.Service.AutofacModule.ModuleRegistration.RegisterApplicationObjects(builder);

            // Set the dependency resolver. This works for both regular
            // WCF services and REST-enabled services.

            AutofacHostFactory.Container = builder.Build();

            // SOAP
            //var svcHostFactory = new AutofacServiceHostFactory();
            //var soapServiceUrn = string.Format("soap/{0}", typeof(SignInService).Name);
            //var soapServiceRoute = new ServiceRoute(soapServiceUrn, svcHostFactory, typeof(SignInService));
            //RouteTable.Routes.Add(soapServiceRoute);

            // REST
            //var webHostFactory = new AutofacWebServiceHostFactory();
            //var restServiceUrn = string.Format("rest/{0}", typeof(SignInService).Name);
            //var restServiceRoute = new ServiceRoute("SignInService", webHostFactory, typeof(SignInService));
            //RouteTable.Routes.Add(restServiceRoute);
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