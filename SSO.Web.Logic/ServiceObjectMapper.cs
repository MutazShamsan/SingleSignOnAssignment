using SSO.DataModel;

namespace SSO.Web.Logic
{
    public static class ServiceObjectMapper
    {
        public static void MapWebServiceObjects()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<LoginRequsetModel, WebServiceAuthService.LoginRequsetModel>();
                cfg.CreateMap<WebServiceAuthService.LoginResposeModel, LoginResposeModel>();

                cfg.CreateMap<RegistrationRequestModel, WebServiceAuthService.RegistrationRequestModel>();
                cfg.CreateMap<WebServiceAuthService.RegistrationResposeModel, RegistrationResposeModel>();
            });
        }
    }
}
