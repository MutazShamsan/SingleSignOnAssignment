using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SSO.Web.App.IoC;
using System.Reflection;
using System.Web.Mvc;

namespace SSO.Web.App
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Login/Index"),
            });
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            
            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            ModuleRegistration.RegisterApplicationObjects(builder);
            //builder.RegisterType<MyActionFilter>().PropertiesAutowired();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            app.UseAutofacMiddleware(container);  // 1 - setting up autofac middleware (Autofac.Integration.Owin.dll)
       
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}