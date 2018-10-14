using SSO.DataModel;
using System.Threading.Tasks;

namespace SSO.Interfaces
{
    public interface IRegistrationManagement : IServiceResponse<RegistrationResposeModel>
    {
        Task<RegistrationResposeModel> RegisterNewUser(RegistrationRequestModel request);
    }
}
