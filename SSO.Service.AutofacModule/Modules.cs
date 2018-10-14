using Autofac;
using SSO.Common;
using SSO.Interfaces;
using SSO.Log4Net;
using SSO.Service.DataContext;
using SSO.Service.Logic;
using SSO.Service.Repositories;
using System.Data;
using System.Data.SqlClient;

namespace SSO.Service.AutofacModule
{
    /// <summary>
    /// Autofac Modules registration
    /// </summary>
    /// 
    public class DatabaseConnectionModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppDbConnection"].ConnectionString)).As<IDbConnection>();
        }
    }

    public class SignInManagementModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new LoginManagement(x.Resolve<IAppUserRepository>(), x.Resolve<ILogger>(), x.Resolve<ICrypto>(), x.Resolve<ICacheManagement>())).As<ILoginManagement>();
        }
    }

    public class NewUserManagementModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new RegistrationManagement(x.Resolve<IAppUserRepository>(), x.Resolve<ILogger>(), x.Resolve<ICrypto>())).As<IRegistrationManagement>();
        }
    }

    public class RepositoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new AppUserRepository(x.Resolve<IDataContext>())).As<IAppUserRepository>();
        }
    }

    public class DataContextModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new AppDataContext(x.Resolve<IDbConnection>())).As<IDataContext>();
        }
    }

    public class LoggertModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new Log4NetLogger()).As<ILogger>();
        }
    }

    public class CryptoModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new Crypto()).As<ICrypto>();
        }
    }

    public class CacheManagementModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(x => new MemoryCacheManagement()).As<ICacheManagement>();
        }
    }
}
