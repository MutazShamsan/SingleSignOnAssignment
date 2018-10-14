using Autofac;
using Autofac.Integration.Mvc;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SSO.WebApp1.ActionFilters;
using SSO.WebApp1.IoC;
using System.Reflection;
using System.Web.Mvc;

namespace SSO.WebApp1
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Login/Index"),

            });

            var builder = new ContainerBuilder();

            // Register your MVC controllers. (MvcApplication is the name of
            // the class in Global.asax.)
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(typeof(MvcApplication).Assembly);
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            ModuleRegistration.RegisterApplicationObjects(builder);
            builder.RegisterType<MyActionFilter>().PropertiesAutowired();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            app.UseAutofacMiddleware(container);  // 1 - setting up autofac middleware (Autofac.Integration.Owin.dll)
            //container.UseAutofacMvc(); //
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            //app.UseAutofacMiddleware(container);
        }
    }
}