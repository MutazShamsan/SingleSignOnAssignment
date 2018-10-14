using SSO.DataModel;
using System.ServiceModel;
using System.Threading.Tasks;

namespace SSO.WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IAuthWcfService" in both code and config file together.
    [ServiceContract]
    public interface IAuthWcfService
    {
        [OperationContract]
        Task<RegistrationResposeModel> RegisterNewUserContract(RegistrationRequestModel request);

        [OperationContract]
        Task<LoginResposeModel> LoginContract(LoginRequsetModel request);

        [OperationContract]
        Task<LoginResposeModel> LogoutContract(LoginRequsetModel request);
    }
}
