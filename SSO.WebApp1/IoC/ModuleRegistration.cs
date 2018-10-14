﻿using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSO.WebApp1.IoC
{
    public static class ModuleRegistration
    {
        public static void RegisterApplicationObjects(ContainerBuilder builder)
        {
            builder.RegisterModule(new LoggertModule());
            builder.RegisterModule(new AuthServiceModule());
        }
    }
}