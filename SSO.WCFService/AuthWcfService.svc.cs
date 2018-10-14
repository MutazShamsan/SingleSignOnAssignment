using SSO.DataModel;
using SSO.Interfaces;
using System.ServiceModel;
using System.Threading.Tasks;

namespace SSO.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "AuthWcfService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select AuthWcfService.svc or AuthWcfService.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class AuthWcfService : IAuthWcfService
    {
        #region Properties
        private readonly ILoginManagement _loginManagement;
        private readonly IRegistrationManagement _registrationManagement;
        #endregion

        public AuthWcfService(ILoginManagement loginManagement, IRegistrationManagement registrationManagement)
        {
            _loginManagement = loginManagement;
            _registrationManagement = registrationManagement;
        }

        /// <summary>
        /// Service End Point to register new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<RegistrationResposeModel> RegisterNewUserContract(RegistrationRequestModel request)
        {
            return _registrationManagement.RegisterNewUser(request);
        }

        /// <summary>
        /// Service End Point to login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Task<LoginResposeModel> LoginContract(LoginRequsetModel request)
        {
            return _loginManagement.LoginUser(request);
        }

        public Task<LoginResposeModel> LogoutContract(LoginRequsetModel request)
        {
            return _loginManagement.LogoutUser(request);
        }
    }
}
