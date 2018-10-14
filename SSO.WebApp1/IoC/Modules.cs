using Autofac;
using SSO.Interfaces;
using SSO.Log4Net;
using SSO.Web.Logic;

namespace SSO.WebApp1.IoC
{
    public class LoggertModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new Log4NetLogger()).As<ILogger>();
        }
    }

    public class AuthServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new WcfAuthProvider(x.Resolve<ILogger>())).As<IAuthProvider>();
        }
    }
}