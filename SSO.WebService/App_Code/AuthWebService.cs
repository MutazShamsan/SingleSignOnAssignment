using SSO.Common;
using SSO.DataModel;
using SSO.Interfaces;
using SSO.Log4Net;
using SSO.Service.DataContext;
using SSO.Service.Logic;
using SSO.Service.Repositories;
using System.Data.SqlClient;
using System.Web.Services;

/// <summary>
/// Summary description for AuthWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class AuthWebService : System.Web.Services.WebService
{
    private ILoginManagement _loginManagement;
    private IRegistrationManagement _registrationManagement;
    private IAppUserRepository _appUserRepository;
    private IDataContext _dataContext;
    private ILogger _logger;
    private ICrypto _crypto;
    private ICacheManagement _cacheManagement;

    public AuthWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 

        //Not implemention Ioc
        _dataContext = new AppDataContext(new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["AppDbConnection"].ConnectionString));
        _crypto = new Crypto();
        _cacheManagement = new MemoryCacheManagement();
        _appUserRepository = new AppUserRepository(_dataContext);
        SSO.Common.StaticMembers.ApplicationDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
        _logger = new Log4NetLogger();
        _registrationManagement = new RegistrationManagement(_appUserRepository, _logger, _crypto);
        _loginManagement = new LoginManagement(_appUserRepository, _logger, _crypto, _cacheManagement);
    }

    [WebMethod]
    public RegistrationResposeModel RegisterNewUserContract(RegistrationRequestModel composite)
    {
        return _registrationManagement.RegisterNewUser(composite).Result;
    }

    [WebMethod]
    public LoginResposeModel LoginContract(LoginRequsetModel composite)
    {
        return _loginManagement.LoginUser(composite).Result;
    }

    [WebMethod]
    public LoginResposeModel LogoutContract(LoginRequsetModel composite)
    {
        return _loginManagement.LogoutUser(composite).Result;
    }

}
