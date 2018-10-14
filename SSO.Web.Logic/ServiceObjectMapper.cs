using SSO.DataModel;

namespace SSO.Web.Logic
{
    /// <summary>
    /// Create the mapper class that should only run once in the App lifecycle
    /// 
    /// </summary>
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
