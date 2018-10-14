using SSO.DataModel;
using SSO.Interfaces;
using SSO.Web.Logic.WcfAuthService;
using System;

namespace SSO.Web.Logic
{
    public class WcfAuthProvider : IAuthProvider
    {
        private readonly WcfAuthService.IAuthWcfService _authService;
        private readonly ILogger _logger;

        public WcfAuthProvider(ILogger logger)
        {
            _logger = logger;

            try
            {
                _authService = new AuthWcfServiceClient();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to connect to service");
            }
        }

        public RegistrationResposeModel Register(RegistrationRequestModel request)
        {
            return _authService.RegisterNewUserContract(request);
        }

        public LoginResposeModel Login(LoginRequsetModel request)
        {
            return _authService.LoginContract(request);
        }

        public LoginResposeModel Logout(LoginRequsetModel request)
        {
            return _authService.LogoutContract(request);
        }
    }
}
