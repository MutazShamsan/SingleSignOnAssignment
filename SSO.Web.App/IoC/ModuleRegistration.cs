using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSO.Web.App.IoC
{
    public static class ModuleRegistration
    {
        /// <summary>
        /// Register the Autofac modules
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterApplicationObjects(ContainerBuilder builder)
        {
            builder.RegisterModule(new LoggertModule());
            builder.RegisterModule(new AuthServiceModule());
        }
    }
}