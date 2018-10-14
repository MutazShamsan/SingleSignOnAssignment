using SSO.DataModel;

namespace SSO.Interfaces
{
    public interface IAuthProvider
    {
        LoginResposeModel Login(LoginRequsetModel request);
        LoginResposeModel Logout(LoginRequsetModel request);
        RegistrationResposeModel Register(RegistrationRequestModel request);
    }
}
