using Autofac;

namespace SSO.Service.AutofacModule
{
    public static class ModuleRegistration
    {
        /// <summary>
        /// Single function to register all modules for Autofac container
        /// </summary>
        /// <param name="builder"></param>
        public static void RegisterApplicationObjects(ContainerBuilder builder)
        {
            builder.RegisterModule(new LoggertModule());
            builder.RegisterModule(new CacheManagementModule());
            builder.RegisterModule(new CryptoModule());
            builder.RegisterModule(new DatabaseConnectionModule());
            builder.RegisterModule(new DataContextModule());
            builder.RegisterModule(new RepositoryModule());
            builder.RegisterModule(new SignInManagementModule());
            builder.RegisterModule(new NewUserManagementModule());
        }
    }
}
