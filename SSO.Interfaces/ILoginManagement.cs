using SSO.DataModel;
using System.Threading.Tasks;

namespace SSO.Interfaces
{
    public interface ILoginManagement : IServiceResponse<LoginResposeModel>
    {
        Task<LoginResposeModel> LoginUser(LoginRequsetModel request);
        Task<LoginResposeModel> LogoutUser(LoginRequsetModel request);
    }
}
