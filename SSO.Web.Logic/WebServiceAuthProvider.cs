using SSO.DataModel;
using SSO.Interfaces;
using System;

namespace SSO.Web.Logic
{
    public class WebServiceAuthProvider : IAuthProvider
    {
        private SSO.Web.Logic.WebServiceAuthService.AuthWebServiceSoap _authService;
        private readonly ILogger _logger;

        public WebServiceAuthProvider(ILogger logger)
        {
            _logger = logger;

            try
            {
                _authService = new WebServiceAuthService.AuthWebServiceSoapClient();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to connect to service");
            }
        }

        public RegistrationResposeModel Register(RegistrationRequestModel request)
        {
            //return _authService.RegisterNewUserContract(request);

            return AutoMapper.Mapper.Map<RegistrationResposeModel>(
                _authService.RegisterNewUserContract(AutoMapper.Mapper.Map<WebServiceAuthService.RegistrationRequestModel>(request)));
        }

        public LoginResposeModel Login(LoginRequsetModel request)
        {
            return AutoMapper.Mapper.Map<LoginResposeModel>(
                _authService.LoginContract(AutoMapper.Mapper.Map<WebServiceAuthService.LoginRequsetModel>(request)));
        }

        public LoginResposeModel Logout(LoginRequsetModel request)
        {
            return AutoMapper.Mapper.Map<LoginResposeModel>(
                _authService.LogoutContract(AutoMapper.Mapper.Map<WebServiceAuthService.LoginRequsetModel>(request)));
        }
    }
}
